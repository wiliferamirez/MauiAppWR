using System.Text.Json;

namespace MauiAppWR.Models
{
    internal class Recordatorio
    {
        public string Id { get; set; }
        public string Texto { get; set; }
        public DateTime FechaHora { get; set; }
        public bool Activo { get; set; }

        private static string FilePath => Path.Combine(FileSystem.AppDataDirectory, "recordatorios.json");

        public Recordatorio()
        {
            Id = Guid.NewGuid().ToString();
            FechaHora = DateTime.Now;
            Activo = true;
        }

        public static List<Recordatorio> LoadAll()
        {
            if (!File.Exists(FilePath))
                return new List<Recordatorio>();

            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Recordatorio>>(json) ?? new List<Recordatorio>();
        }

        public static void SaveAll(List<Recordatorio> recordatorios)
        {
            var json = JsonSerializer.Serialize(recordatorios, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }
    }
}
