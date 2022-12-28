using System.Text.Json;

namespace FileHandel.Model
{
    public static class DemoDBAction
    {
        public static List<File> ReadList()
        {
            try
            {
                using (var sr = new StreamReader("DB.json"))
                {
                    string s = sr.ReadToEnd();
                    return JsonSerializer.Deserialize<List<File>>(s);
                }
            }
            catch (IOException e)
            {
                throw e;

            }
        }
        public static void WriteList(List<File> files)
        {
            try
            {
                using (var sr = new StreamWriter("DB.json"))
                {
                    var jsonString = JsonSerializer.Serialize(files);
                    sr.Write(jsonString);

                }
            }
            catch (IOException e)
            {
                throw e;

            }


        }
        public static File GetFile(int id)
        {
            return ReadList().Find(item => item.fileID == id);
        }
        public static void InsetFile(File file)
        {

            List<File> updateList = ReadList();
            file.fileID = updateList.Count() + 1;
            updateList.Add(file);
            WriteList(updateList);
        }
        public static void InsetFiles(List<File> files)
        {

            List<File> updateList = ReadList();
            int listLenght = updateList.Count()+1;
            files.ForEach(file => file.fileID =  ++listLenght );
            updateList.AddRange(files);
            WriteList(updateList);
        }
        public static void UpdateFile(File file)
        {

            List<File> updateList = ReadList();
            File fileToUpdate = updateList.Find(item => item.fileID == file.fileID);
            if (fileToUpdate != null)
            {
                fileToUpdate.UpdateFields(file);
                WriteList(updateList);
            }
        }
        public static void RemoveFile(File file)
        {

            List<File> updateList = ReadList();
            updateList.Remove(updateList.First(_=>_.fileID==file.fileID));
            WriteList(updateList);
        }
    }
}
