using Infrastructure.Infrastructure.Operations;

namespace Infrastructure.Infrastructure.Services.Storage;

public class Storage
{
    protected delegate bool HasFile(string pathOrContainerName, string fileName);

    protected async Task<string> FileRenameAsync(string pathOrContainerName, string fileName, HasFile hasFileMethod)
    {
        return await Task.Run(() =>
        {
            string oldName = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);
            string newFileName = $"{NameOperation.CharacterRegulatory(oldName)}-1{extension}";
            bool fileIsExists = false;
            int fileIndex = 0;

            do
            {
                if (hasFileMethod(pathOrContainerName, newFileName))
                {
                    fileIsExists = true;
                    fileIndex++;
                    newFileName = $"{NameOperation.CharacterRegulatory(oldName + "-" + fileIndex)}{extension}";
                }
                else
                    fileIsExists = false;
            } while (fileIsExists);

            return newFileName;
        });
    }
}
