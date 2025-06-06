﻿using System;
using Microsoft.Data.SqlClient;

namespace DataAcessLayer
{
    public class NotesDAL
    {
        public List<(int, string)> GetNotesTitlesAndIDs()
        {
            List<(int, string)> titlesIDs = new List<(int, string)>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlCommand getTitlesAndIDsCMD = new SqlCommand("SELECT NoteID, NoteTitle FROM Notes", conn);
                SqlDataReader reader = getTitlesAndIDsCMD.ExecuteReader();
                while (reader.Read())
                {
                    titlesIDs.Add(((int)reader[0], reader[1].ToString()));
                }
                reader.Close();
            }
            return titlesIDs;
        }

        public string GetNoteContent(int noteID)
        {
            string noteContents;
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlCommand getContentsCMD = new SqlCommand($"SELECT NoteContent FROM Notes WHERE NoteID = {noteID}", conn);
                noteContents = getContentsCMD.ExecuteScalar().ToString();
            }
            return noteContents;
        }

        public bool CreateNote(string noteTitle, string noteContent)
        {
            bool isNoteCreated = false;
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlCommand createNoteCMD = new SqlCommand("INSERT INTO Notes (NoteTitle, NoteContent, DateCreated, DateUpdated) VALUES (@noteTitle, @noteContent, GETDATE(), GETDATE())", conn);
                createNoteCMD.Parameters.AddWithValue("@noteTitle", noteTitle);
                createNoteCMD.Parameters.AddWithValue("@noteContent", noteContent);
                if (createNoteCMD.ExecuteNonQuery() != 0)
                    isNoteCreated = true;
            }
            return isNoteCreated;
        }

        public bool UpdateNoteContent(int noteID, string noteContents)
        {
            bool isNoteUpdated = false;
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlCommand updateNoteCMD = new SqlCommand($"UPDATE Notes SET NoteContent = @noteContent, DateUpdated = GETDATE() WHERE NoteID = {noteID}", conn);
                updateNoteCMD.Parameters.AddWithValue("@noteContent", noteContents);
                if (updateNoteCMD.ExecuteNonQuery() != 0)
                    isNoteUpdated = true;
            }
            return isNoteUpdated;
        }

        public bool DeleteNote(int noteID)
        {
            bool isNoteDeleted = false;
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlCommand deleteNoteCMD = new SqlCommand($"DELETE FROM Notes WHERE NoteID = {noteID}", conn);
                if (deleteNoteCMD.ExecuteNonQuery() != 0)
                    isNoteDeleted = true;
            }
            return isNoteDeleted;
        }

        public List<(int, string)> GetFilteredNotes(string tag)
        {
            List<(int, string)> titlesIDs = new List<(int, string)>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlCommand getFilteredNotesCMD = new SqlCommand("SELECT Notes.NoteID, NoteTitle FROM Notes JOIN NotesTags ON Notes.NoteID = NotesTags.NoteID JOIN Tags ON Tags.TagID = NotesTags.TagID WHERE TagContent = @tag", conn);
                getFilteredNotesCMD.Parameters.AddWithValue("@tag", tag);
                SqlDataReader reader = getFilteredNotesCMD.ExecuteReader();
                while (reader.Read())
                {
                    titlesIDs.Add(((int)reader[0], reader[1].ToString()));
                }
                reader.Close();
            }
            return titlesIDs;
        }

        public List<(int, string)> GetNotesByContent(string content)
        {
            List<(int, string)> titlesIDs = new List<(int, string)>();
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                SqlCommand getNotesCMD = new SqlCommand("SELECT NoteID, NoteTitle FROM Notes WHERE NoteContent LIKE @content", conn);
                getNotesCMD.Parameters.AddWithValue("@content", "%" + content + "%");
                SqlDataReader reader = getNotesCMD.ExecuteReader();
                while (reader.Read())
                {
                    titlesIDs.Add(((int)reader[0], reader[1].ToString()));
                }
                reader.Close();
            }
            return titlesIDs;
        }
    }
}   
