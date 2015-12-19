using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Rewrittable
{
	public class VoteMapper : UpdatableDataMapper<Vote>
	{
		public VoteMapper(string connectionString) : base(connectionString)
		{
			TableName = "vote";
			Attributes = new string[] { "postId", "voteTypeId", "creationDate", "userId"};
		}

		public override Vote GetByPostAndUser(int postId, int userId)
		{
			return new Vote { };
		}

		public override int Insert(Vote vote) { throw new NotImplementedException("not supported in this project"); }
		public override int Update(Vote vote) { throw new NotImplementedException("not supported in this project"); }

		public override Vote Map(MySqlDataReader reader)
		{
			if (reader.Read() && reader.HasRows)
			{
				int v_id, v_postId, v_voteType;
				DateTime v_date;
				if (!reader.IsDBNull(0)) { v_id = reader.GetInt32(0); }
				else { v_id = 0; }
				if (!reader.IsDBNull(0)) { v_postId = reader.GetInt32(1); }
				else { v_postId = 0; }
				if (!reader.IsDBNull(0)) { v_voteType = reader.GetInt32(2); }
				else { v_voteType = 0; }
				if (!reader.IsDBNull(0)) { v_date = reader.GetDateTime(3); }
				else { v_date = DateTime.MinValue; }
				return new Vote
				{
					PostId = v_postId,
					Date = v_date,
					VoteType = v_voteType
				};
			}
			return null;
		}
	}
}
