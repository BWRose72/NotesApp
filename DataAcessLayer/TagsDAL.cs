using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace DataAcessLayer
{
    public class TagsDAL
    {
        public List<string> GetAllTags()
        {
            List<string> tags = new List<string>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlCommand getTagsCMD = new SqlCommand($"SELECT TagContent FROM Tags", conn);
                SqlDataReader reader = getTagsCMD.ExecuteReader();
                while (reader.Read())
                {
                    tags.Add(reader[0].ToString());
                }
                reader.Close();
            }
            return tags;
        }

        public List<string> GetNoteTags(int noteID)
        {
            List<string> tags = new List<string>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlCommand getTagsCMD = new SqlCommand($"SELECT TagContent FROM Tags JOIN NotesTags ON Tags.TagID = NotesTags.TagID WHERE NoteID = {noteID}", conn);
                SqlDataReader reader = getTagsCMD.ExecuteReader();
                while (reader.Read())
                {
                    tags.Add(reader[0].ToString());
                }
                reader.Close();
            }
            return tags;
        }

        public bool CreateTag(string tagContent)
        {
            bool isTagCreated = false;
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlCommand createTagCMD = new SqlCommand("INSERT INTO Tags (TagContent) VALUES (@tagContent)", conn);
                createTagCMD.Parameters.AddWithValue("@tagContent", tagContent);
                if (createTagCMD.ExecuteNonQuery() != 0)
                    isTagCreated = true;
            }
            return isTagCreated;
        }

        public bool AddTagToNote(int noteID, int tagID)
        {
            bool isTagAdded = false;
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlCommand addTagCMD = new SqlCommand($"INSERT INTO NotesTags VALUES ({noteID}, {tagID})", conn);
                if (addTagCMD.ExecuteNonQuery() != 0)
                    isTagAdded = true;
            }
            return isTagAdded;
        }

        public bool DoesNoteHaveTag(int noteID, int tagID)
        {
            bool noteHasTag = false;
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlCommand noteHasTagCMD = new SqlCommand($"SELECT COUNT(*) FROM NotesTags WHERE NoteID = {noteID} AND TagID = {tagID})", conn);
                if ((int)noteHasTagCMD.ExecuteScalar() != 0)
                    noteHasTag = true;
            }
            return noteHasTag;
        }

        public bool DeleteTag(int tagID)
        {
            bool tagDeleted = false;
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlCommand deleteTagCMD = new SqlCommand($"DELETE FROM Tags WHERE TagID = {tagID}", conn);
                if (deleteTagCMD.ExecuteNonQuery() > 0)
                {
                    tagDeleted = true;
                }
            }
            return tagDeleted;
        }   
    }
}
