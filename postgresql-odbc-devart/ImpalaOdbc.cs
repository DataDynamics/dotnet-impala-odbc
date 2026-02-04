using System.Data;
using Devart.Data.Universal;
using log4net;

namespace YourNamespace;

internal class ImpalaOdbc
{

    private static readonly ILog log = LogManager.GetLogger(typeof(ImpalaOdbc));

    private static void Main(string[] args)
    {
        // Impala ODBC 연결 문자열 설정
        var connectionString =
            "Provider=Odbc;Driver={Cloudera ODBC Driver for Impala};Host=hdw1.ktwings.dd.io;Port=21050;Database=default;UID=;PWD=;";
        // UniConnection 객체 생성
        using (var connection = new UniConnection(connectionString))
        {
            try
            {
                // 연결 열기
                connection.Open();
                log.Info("Connection to Impala established successfully.");
                // 실행할 쿼리
                var query = "SELECT * FROM default.test LIMIT 10";
                // UniCommand 객체 생성
                using (var command = new UniCommand(query, connection))
                {
                    // 데이터 읽기
                    using (var reader = command.ExecuteReader())
                    {
                        // 결과 출력
                        while (reader.Read())
                        {
                            for (var i = 0; i < reader.FieldCount; i++)
                                log.Info($"{reader.GetName(i)}: {reader[i]} \t");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 예외 처리
                log.Warn($"An error occurred: {ex.Message}");
            }
            finally
            {
                // 연결 닫기
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    log.Info("Connection closed.");
                }
            }
        }
    }
}