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
                            throw new ArchiveParsingException($"Invalid VPK: {filename}");
                        }
                    }
                }

                Directories.AddRange(_reader.ReadDirectories(this));
            }
            catch (Exception e)
            {
                MessageBox.Show($"Invalid VPK:\n\n{filename}");
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
            var fileBaseName = filename.Split('_')[0];
            foreach (var file in Directory.GetFiles(Path.GetDirectoryName(filename)))
            {
                if (file.Split('_')[0] != fileBaseName || file == filename)
                    continue;
                var fi = new FileInfo(file);
                string[] spl = file.Split('_');
                var partIdx = Int32.Parse(spl[spl.Length - 1].Split('.')[0]);
                Parts.Add(new ArchivePart((uint)fi.Length, partIdx, file));
            }
            Parts.Add(new ArchivePart((uint) new FileInfo(filename).Length, -1, filename));
            Parts = Parts.OrderBy(p => p.Index).ToList();
        }
    }
}
