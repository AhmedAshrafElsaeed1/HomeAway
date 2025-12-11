using System.Text.Json;

namespace front_end.Helpers
{
    public static class JwtHelper
    {
        public static Dictionary<string, object>? DecodePayload(string token)
        {
            try
            {
                var parts = token.Split('.');
                if (parts.Length < 3) return null;

                var payload = parts[1];
                var jsonBytes = Convert.FromBase64String(AddPadding(payload));
                var json = System.Text.Encoding.UTF8.GetString(jsonBytes);

                return JsonSerializer.Deserialize<Dictionary<string, object>>(json);
            }
            catch
            {
                return null;
            }
        }

        private static string AddPadding(string base64)
        {
            return base64.PadRight(base64.Length + (4 - base64.Length % 4) % 4, '=');
        }
    }
}
