using log4net;
using Npgsql;

namespace YourNamespace;

internal class Program
{

        
    private static readonly ILog log = LogManager.GetLogger(typeof(Program));
    
    
    // 연결 문자열 설정
    private const string ConnectionString = "Host=10.0.1.60;Username=scm;Password=dd98969321;Database=scm";

    private static async Task Main(string[] args)
    {
        log.Info("PostgreSQL 접속 시도 중...");

        try
        {
            // 1. NpgsqlDataSource 생성 (8.0 버전 권장 방식)
            // 이 객체는 애플리케이션 수명 동안 한 번만 생성하여 재사용하는 것이 성능에 좋습니다.
            using var dataSource = NpgsqlDataSource.Create(ConnectionString);

            // 2. 쿼리 실행 (public.audits 테이블 10건 조회)
            const string sql = "SELECT * FROM public.audits LIMIT 10";

            using var command = dataSource.CreateCommand(sql);
            using var reader = await command.ExecuteReaderAsync();

            log.Info("--- 조회 결과 ---");

            // 컬럼 헤더 출력 (첫 번째 행 읽기 전)
            for (var i = 0; i < reader.FieldCount; i++) Console.Write($"{reader.GetName(i)}\t");
            log.Info("\n" + new string('-', 50));

            // 데이터 출력
            while (await reader.ReadAsync())
            {
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    // null 값 처리 포함 출력
                    var value = reader.IsDBNull(i) ? "NULL" : reader.GetValue(i).ToString();
                    log.Info($"{value}\t");
                }

                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            log.Info($"오류 발생: {ex.Message}");
        }

        log.Info("\n작업 완료. 종료하려면 아무 키나 누르세요.");
        Console.ReadKey();
    }
}