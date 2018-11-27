using Pieka.Persistance;
using NUnit.Framework;
using System.IO;

[TestFixture]
public class RepositoryTest
{
    private string path = Repository.DefaultLocation + typeof(Data).FullName + ".json";
    private string backupPath = Repository.DefaultLocation + typeof(Data).FullName + "_backup.json";
    [SetUp]
    public void Init()
    {
        File.Delete(path);
        File.Delete(backupPath);
    }

    [Test]
    public void ShouldSaveFileAndBackup()
    {
        var promise = Repository.Save(new Data());
        promise.Wait();
        Assert.True(File.Exists(path));
        Assert.True(File.Exists(backupPath));
    }

    [Test]
    public void ShouldLoadFile()
    {
        var json = "{ \"Info\" : \"Hello from test\" }";
        File.WriteAllText(path, json);
        Data result = null;
        var promise = Repository.Load<Data>(r => result = r);
        promise.Wait();
        Assert.AreEqual(result.Info, "Hello from test");
    }

    [Test]
    public void ShouldLoadFileFromBackupWhenJsonIsMissing()
    {
        var json = "{ \"Info\" : \"Hello from test\" }";
        File.WriteAllText(backupPath, json);
        Data result = null;
        var promise = Repository.Load<Data>(r => result = r);
        promise.Wait();
        Assert.AreEqual(result.Info, "Hello from test");
    }


    [Test]
    public void ShouldLoadOriginalFileNotBackup()
    {
        var json = "{ \"Info\" : \"Original\" }";
        var backup = "{ \"Info\" : \"Backup\" }";
        File.WriteAllText(path, json);
        File.WriteAllText(backupPath, backup);
        Data result = null;
        var promise = Repository.Load<Data>(r => result = r);
        promise.Wait();
        Assert.AreEqual(result.Info, "Original");
    }


    [Test]
    public void ShouldLoadBackupWhenOriginalIsCorrupted()
    {
        var json = "{ \"Info\" : \"Original\" ";
        var backup = "{ \"Info\" : \"Backup\" }";
        File.WriteAllText(path, json);
        File.WriteAllText(backupPath, backup);
        Data result = null;
        var promise = Repository.Load<Data>(r => result = r);
        promise.Wait();
        Assert.AreEqual(result.Info, "Backup");
    }


    [Test]
    public void ShouldReturnNullIfFileDoesntExist()
    {
        Data result = null;
        var promise = Repository.Load<Data>(r => result = r);
        promise.Wait();
        Assert.Null(result);
    }

    private class Data
    {
        public string Info = "Hello";
    }
}
