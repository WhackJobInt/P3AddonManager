﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpVPK.Exceptions;
using SharpVPK.V1;

namespace SharpVPK
{
    public class VpkArchive
    {
        public List<VpkDirectory> Directories { get; set; }
        public bool IsMultiPart { get; set; }
        public bool IsV1 { get; set; }
        public bool IsV2 { get; set; }
        private VpkReaderBase _reader;
        internal List<ArchivePart> Parts { get; set; }
        internal string ArchivePath { get; set; }

        public VpkArchive()
        {
            Directories = new List<VpkDirectory>();
        }

        public void Load(string filename, VpkVersions.Versions version = VpkVersions.Versions.V1)
        {
            try
            {
                ArchivePath = filename;
                IsMultiPart = filename.EndsWith("_dir.vpk");
                if (IsMultiPart)
                    LoadParts(filename);
                if (version == VpkVersions.Versions.V1)
                {
                    IsV1 = true;
                    _reader = new VpkReaderV1(filename);
                    var hdr = _reader.ReadArchiveHeader();
                    if (!hdr.Verify())
                    {
                        IsV1 = false;
                        IsV2 = true;
                        _reader = new V2.VpkReaderV2(filename);
                        hdr = _reader.ReadArchiveHeader();
                        if (!hdr.Verify())
                        {
                            throw new ArchiveParsingException($"Invalid VPK:\n\n{filename}\n\nRemove or rename this VPK, then reload the program.");
                        }
                    }
                }

                Directories.AddRange(_reader.ReadDirectories(this));
            }
            catch (Exception e)
            {
                MessageBox.Show($"Invalid VPK:\n\n{filename}\n\nRemove or rename this VPK, then reload the program.");
                throw;
            }
        }

        public void Load(byte[] file, VpkVersions.Versions version = VpkVersions.Versions.V1)
        {
            if (version == VpkVersions.Versions.V1)
                _reader = new VpkReaderV1(file);
            else if (version == VpkVersions.Versions.V2)
                _reader = new V2.VpkReaderV2(file);
            var hdr = _reader.ReadArchiveHeader();
            if (!hdr.Verify())
                throw new ArchiveParsingException("Invalid archive header");
            Directories.AddRange(_reader.ReadDirectories(this));
        }

        private void LoadParts(string filename)
        {
            Parts = new List<ArchivePart>();

            var fileBaseName = Path.GetFileNameWithoutExtension(filename)?.Split('_')[0];
            if (fileBaseName == null)
                throw new ArgumentException("Invalid filename format", nameof(filename));

            var directory = Path.GetDirectoryName(filename);
            if (directory == null)
                throw new ArgumentException("Invalid directory for the provided filename", nameof(filename));

            var currentFile = new FileInfo(filename);
            var files = Directory.GetFiles(directory)
                                 .Where(file => !string.Equals(file, filename, StringComparison.OrdinalIgnoreCase))
                                 .Where(file => Path.GetFileNameWithoutExtension(file)?.Split('_')[0] == fileBaseName);

            foreach (var file in files)
            {
                var partIndex = ExtractPartIndex(file);
                if (partIndex.HasValue)
                {
                    var fileInfo = new FileInfo(file);
                    Parts.Add(new ArchivePart((uint)fileInfo.Length, partIndex.Value, file));
                }
            }

            Parts.Add(new ArchivePart((uint)currentFile.Length, -1, filename));
            Parts = Parts.OrderBy(p => p.Index).ToList();
        }

        private int? ExtractPartIndex(string filePath)
        {
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            var segments = fileName?.Split('_');
            if (segments == null || segments.Length < 2)
                return null;

            var indexSegment = segments[^1];
            return int.TryParse(indexSegment, out var index) ? index : (int?)null;
        }
    }
}
