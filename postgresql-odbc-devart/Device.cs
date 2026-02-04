using Newtonsoft.Json;

namespace postgresql_odbc_devart;

internal class Device
{
    [JsonProperty("device_id")] // JSON의 device_id를 DeviceId에 매핑
    public string DeviceId { get; set; }

    [JsonIgnore] // 이 속성은 JSON 결과에 포함하지 않음
    public string InternalCode { get; set; }
}