using LibGit2Sharp;

namespace GetGitChange
{
    internal class Program
    {

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("please give args  source  target ,ex: exe E:\\git E:\\save");
                return;
            }
            string repoPath = args[0];
            string targetPath = args.Length > 1 ? args[1] : Path.Combine(AppContext.BaseDirectory, "temp");

            using (var repo = new Repository(repoPath))
            {
                RepositoryStatus status = repo.RetrieveStatus();

                List<string> deletePaths = new List<string>();

                var list = status.Where(t => t.State == FileStatus.NewInWorkdir || t.State == FileStatus.ModifiedInWorkdir || t.State == FileStatus.DeletedFromWorkdir);
                foreach (StatusEntry entry in status.Where(t => t.State != FileStatus.Ignored))
                {
                    if (entry.State == FileStatus.DeletedFromWorkdir)
                    {
                        deletePaths.Add(entry.FilePath);
                    }
                    else
                    {
                        var path = Path.Combine(repo.Info.WorkingDirectory, entry.FilePath);
                        if (File.Exists(path))
                        {
                            var newPath = Path.Combine(targetPath, entry.FilePath);
                            var dir = Path.GetDirectoryName(newPath);
                            if (dir != null && !Directory.Exists(dir))
                            {
                                Directory.CreateDirectory(dir);
                            }
                            File.Copy(path, newPath, true);
                            Console.WriteLine($"Copied: {path} to {newPath}");
                        }
                    }
                }
                if (deletePaths.Any())
                {
                    File.WriteAllText(Path.Combine(targetPath, "delete.txt"), string.Join(Environment.NewLine, deletePaths));
                }
                Console.WriteLine("Changes copied to target directory.");
            }
            Console.ReadKey();
        }
    }
}
