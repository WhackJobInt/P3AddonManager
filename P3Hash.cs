using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P3AddonManager
{
    public static class P3Hash
    {
        public static string Current_Client = "";
        public static string Current_Server = "";
        public static string Current_Exe = "";

        public enum P3Version
        {
            Unknown = 0,

            NoDRM,
            VicNoDRM,

            v1_10,
            v1_11,
            v1_12,

            Steam,
            Russian,

            OldAngelv1_00,
            OldAngelv1_01,

            Ultrapatchv1_0_0,
            Ultrapatchv1_1_4,
            Ultrapatchv1_2_0,

            Angelv1_0_0,

                ADDON_SUPPORT, //

            ZOOM,

            Angelv1_1_0,

                MAX
        };

        static public int AddonIndex(P3Version version)
        {
            return version - P3Version.ADDON_SUPPORT;
        }

        // You could argue that this could be made better... you are right
        public static string[] P3_Clients =
        {
            "",

            "0161E9B092072207340FCB2EA53D55C2", // no-drm
            "A050E794BAC654C4152678F5618C88AF", // VICSHANN no-drm

            "B7D52C608B7E7D253C6B9C569A98980A", // v1.10 (Retail)
            "FF80BBCDF0A65C6D47A7661A7B6EA198", // v1.11 (Retail)
            "9EB96F27BC7DDC5FB1ADD7323F5BE082", // v1.12 (Retail)

            "1456c8ca04610fb201b7d1376d0559f1", // Steam v1.12 (Pre-ZOOM)
            "37591B701DF629EEF422724AF63C398D", // Russian Retail

            "6B962C410CE6074D73070AD4BD72D7AE", // non-Ultrapatch Angel v1.00
            "04053F8F56A4324866D5DE8B129FB72A", // non-Ultrapatch Angel v1.01

            "039C93D6F10346BCCB64C9962951D436", // Ultrapatch v1.0.0
            "A9CDE0DDB8649C36A4D240F707883FA5", // Ultrapatch v1.1.4
            "707BC3B6045BC487E9C94E5858F146B7", // Ultrapatch v1.2.0

            "934168BAFF17F1126F1922762AEE0B00", // Ultrapatch+Angel v1.0.0

            // -------------------------------------------------------------
            "",                                 // ADDON SUPPORT BEGINS HERE
            // -------------------------------------------------------------

            "0161E9B092072207340FCB2EA53D55C2", // Steam ZOOM (<=1.4)

            "DAF32A5644C76E041D0DA92B51468BA4", // Ultrapatch Angel v1.1.0


            ""
        };
        public static string[] P3_Servers =
        {
            "",

            "EDDA2FC0765C13E40F2570973D39C953", // no-drm
            "EDDA2FC0765C13E40F2570973D39C953", // VICSHANN no-drm

            "491FE144BDE04450C86DC1F5B2F0A6A2", // v1.10 (Retail)
            "A166D897BEB3ABE975D353FEA23E2D9D", // v1.11 (Retail)
            "94DDB5670F121880DEAD40DFF4AB3796", // v1.12 (Retail)

            "2f8d35008f78dff3d9f13ad2550830ab", // Steam v1.12 (Pre-ZOOM)
            "1E3038294A0DB3DA1899BA6464FF50A4", // Russian Retail

            "781DFA7E00EE641A3A6C203CA7B508FA", // non-Ultrapatch Angel v1.00
            "86E68F06D1353ABEAFFC1AE172533B1D", // non-Ultrapatch Angel v1.01

            "CC5223BAAC0077F994A510A93A53A330", // Ultrapatch v1.0.0
            "E3FD8EC69F035417AD1B062E1AC45DFD", // Ultrapatch v1.1.4
            "92F53F52C9B0BB780CCB0CDBCB3911A7", // Ultrapatch v1.2.0

            "73457EAE5F79641D9216E686F1298519", // Ultrapatch+Angel v1.0.0

            // -------------------------------------------------------------
            "",                                 // ADDON SUPPORT BEGINS HERE
            // -------------------------------------------------------------

            "EDDA2FC0765C13E40F2570973D39C953", // Steam ZOOM (<=1.4)

            "2BE3FAEA28551902441009C68933168F", // Ultrapatch Angel v1.1.0


            ""


        };
        public static string[] P3_Exes =
        {
            "",

            "D99738300AAB14306DA0BBABD12949EA", // no-drm
            "D99738300AAB14306DA0BBABD12949EA", // VICSHANN no-drm

            "A9368E113E6C32C09CE8C2B48A434C49", // v1.10 (Retail)
            "A2D924272B10C381C088FCC539C24622", // v1.11 (Retail)
            "E0CCF1F7DCCAA238EA919F036B3A88E4", // v1.12 (Retail)

            "c7bdb6b68eec6265133dc3f54783847e", // Steam v1.12 (Pre-ZOOM)
            "EC0085EFF325BFF92CA7ECEBDFCE1D26", // Russian Retail

            // TODO: do we even need to check for no-drm here....
            "D99738300AAB14306DA0BBABD12949EA", // non-Ultrapatch Angel v1.00
            "D99738300AAB14306DA0BBABD12949EA", // non-Ultrapatch Angel v1.01

            "D99738300AAB14306DA0BBABD12949EA", // Ultrapatch v1.0.0
            "D99738300AAB14306DA0BBABD12949EA", // Ultrapatch v1.1.4
            "D99738300AAB14306DA0BBABD12949EA", // Ultrapatch v1.2.0

            "D99738300AAB14306DA0BBABD12949EA", // Ultrapatch+Angel v1.0.0

            // -------------------------------------------------------------
            "",                                 // ADDON SUPPORT BEGINS HERE
            // -------------------------------------------------------------

            "2AA403F58D03448B1AB973621BD2CA78", // Steam ZOOM (<=1.4)

            "D99738300AAB14306DA0BBABD12949EA", // Ultrapatch Angel v1.1.0


            ""
        };

        public static string GenerateMD5(string filePath)
        {
            // Use ProcessStartInfo to configure the process
            ProcessStartInfo processInfo = new ProcessStartInfo
            {
                FileName = "certutil",
                Arguments = $"-hashfile \"{filePath}\" MD5",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            // Start the process
            using (Process process = Process.Start(processInfo))
            {
                // Read the output
                using (StreamReader reader = process.StandardOutput)
                {
                    string output = reader.ReadToEnd();
                    process.WaitForExit();

                    // Certutil returns a line with the hash followed by spaces
                    string[] lines = output.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

                    // The hash is usually on the second line
                    if (lines.Length > 1)
                    {
                        return lines[1].Trim();
                    }
                    else
                    {
                        throw new InvalidOperationException("Unable to retrieve MD5 hash from Postal 3 file. Does it exist?");
                    }
                }
            }
        }
    }
}
