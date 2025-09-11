using Microsoft.Extensions.Logging;
using System.Text.Json;
using RedditFreeGamesNotifier.Models.Record;

namespace RedditFreeGamesNotifier.Services {
	internal class JsonOP(ILogger<JsonOP> logger) : IDisposable {
		private readonly ILogger<JsonOP> _logger = logger;

		#region path strings
		private readonly string recordsPath = $"{AppDomain.CurrentDomain.BaseDirectory}Records{Path.DirectorySeparatorChar}records.json";
		#endregion

		#region debug strings
		private readonly string debugWrite = "Write records";
		private readonly string debugLoadRecords = "Load previous records";
		#endregion

		internal void WriteData(List<FreeGameRecord> data) {
			try {
				if (data.Count > 0) {
					_logger.LogDebug(debugWrite);
					string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true } );
					File.WriteAllText(recordsPath, string.Empty);
					File.WriteAllText(recordsPath, json);
					_logger.LogDebug($"Done: {debugWrite}");
				} else _logger.LogDebug("No records detected, quit writing records");
			} catch (Exception) {
				_logger.LogError($"Error: {debugWrite}");
				throw;
			} finally {
				Dispose();
			}
		}

		internal List<FreeGameRecord> LoadData() {
			try {
				_logger.LogDebug(debugLoadRecords);
				var content = JsonSerializer.Deserialize<List<FreeGameRecord>>(File.ReadAllText(recordsPath));
				_logger.LogDebug($"Done: {debugLoadRecords}");
				return content;
			} catch (Exception) {
				_logger.LogError($"Error: {debugLoadRecords}");
				throw;
			}
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
