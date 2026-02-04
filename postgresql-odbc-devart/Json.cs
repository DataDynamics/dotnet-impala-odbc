using log4net;
using Newtonsoft.Json.Linq;
using Npgsql;

namespace YourNamespace;

internal class Json
{
    private static readonly ILog log = LogManager.GetLogger(typeof(Json));
    
    private static async Task Main(string[] args)
    {
        log.Info("Json 직렬화 시작");

        string json = "{ 'price': 1500, 'items': ['apple', 'banana'] }";
        JObject jo = JObject.Parse(json);

        // 특정 필드만 추출
        int price = (int)jo["price"];
        string firstItem = (string)jo["items"][0];

        log.Info($"가격: {price}, 첫 번째 아이템: {firstItem}");
    }
}
