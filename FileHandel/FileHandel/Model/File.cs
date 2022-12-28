using System.ComponentModel.DataAnnotations;

namespace FileHandel.Model
{
    public class File
    {
        [Required]
        public int fileID { get; set; }

        [RegularExpression("[a-zA-Zא-ת ]*")]
        public string aouther { get; set; }

        //public DateTime DateAouther { get; set; }

        [Required]
        [RegularExpression("[a-zA-Zא-ת_0-9 ]*")]
        public string fileName { get; set; }

        [Required]
        [RegularExpression("^[-]?[0-9]+(?:\\.[0-9]+)?$")]
        public decimal fileSize { get; set; }

        [Required]
        public string fileSizeFormat { get; set; }

        [Required]
        public string fileType { get; set; }
        public bool isEncoded { get; set; }

        public void UpdateFields(File file)
        {
            aouther=file.aouther;
            fileName=file.fileName;
            fileSize = file.fileSize;
            fileSizeFormat = file.fileSizeFormat;
            fileType= file.fileType;
            isEncoded = file.isEncoded;

        }
        public List<File> SplitFile(File file)
        {
            decimal fileSizeInGB = file.convertSizeToGB();
            List<File> files = new List<File>();
            DemoDBAction.RemoveFile(file);
            Enumerable.Range(0, (int)Math.Floor(fileSizeInGB)).ToList().ForEach(number => {
                files.Add(new File() { fileName =  $"{file.fileName}_{number+1}", fileSize = 1, fileSizeFormat = "GB", fileType = file.fileType, isEncoded = file.isEncoded, aouther = file.aouther });
            });

            decimal otherFileSize = Math.Round(fileSizeInGB - Math.Floor(fileSizeInGB),3);
            if (otherFileSize > 0)
            {
                files.Add(new File() { fileName = $"{file.fileName}_{files.Count() + 1}", fileSize = otherFileSize, fileSizeFormat = "MB", fileType = file.fileType, isEncoded = file.isEncoded, aouther = file.aouther });
            }
            DemoDBAction.InsetFiles(files);
            return DemoDBAction.ReadList();
        }
        public decimal convertSizeToGB()
        {
            string [] sizeFormat = { "Byte", "KB", "MB", "GB" };
            int sizeFormatIndex = sizeFormat.ToList().IndexOf(fileSizeFormat);
            sizeFormat.ToList().Skip(sizeFormatIndex+1).ToList().ForEach(_ => fileSize /= 1024);// splice(sizeFormatIndex + 1).forEach(() => (size /= 1024));
            return fileSize;
        }
    }
}
