using LocalFHIRRepository.ParameterContainer;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalFHIRRepository.Model
{
    public class DataBasePostModel : BasePostModel
    {
        OracleConnection _connection;

        public DataBasePostModel()
        {
            _connection = new OracleConnection() { ConnectionString = Startup.GetSettings("ConnectionString") };
        }

        protected override void OnDispose()
        {
            _connection.Dispose();
            base.OnDispose();
        }

        /// <summary>
        /// POST実行　To Oracle
        /// </summary>
        /// <param name="value"></param>
        /// <returns>実行成否</returns>
        public override bool TryPost(BaseParameterContainer value)
        {
            try
            {
                // Parameterが不正か確認
                if (!value.IsValid()) return false;
                // Insert実行
                return Insert(value);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// OracleへのINSERT実行
        /// </summary>
        /// <param name="value"></param>
        /// <returns>実行成否</returns>
        private bool Insert(BaseParameterContainer value)
        {
            try
            {
                _connection.Open();
                var sb = new StringBuilder();

                sb = new StringBuilder();
                sb.AppendLine($"insert into TESTTABLE");
                sb.AppendLine($"    (FILENAME, JSONDATA)");
                sb.AppendLine($"values");
                sb.AppendLine($"    (:pfilename, :pjsondata)");
                using (var cmd = new OracleCommand(sb.ToString(), _connection) { CommandType = System.Data.CommandType.Text })
                {
                    cmd.Parameters.Add(new OracleParameter()
                    {
                        ParameterName = "pfilename",
                        OracleDbType = OracleDbType.Varchar2,
                        Direction = ParameterDirection.Input,
                        Value = value.SynchronizeFileName(),
                    });
                    cmd.Parameters.Add(new OracleParameter()
                    {
                        ParameterName = "pjsondata",
                        OracleDbType = OracleDbType.Varchar2,
                        Direction = ParameterDirection.Input,
                        Value = value.OutputJson(),
                    });
                    var val = cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
