using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using BusinessLogicLayer;
using Microsoft.VisualBasic;
using System.Collections;
namespace NotesApp
{
    public partial class Form1 : Form
    {
        private readonly NotesServices noteServices;
        private readonly TagsServices tagsServices;

        public Form1()
        {
            InitializeComponent();
            noteServices = new NotesServices();
            tagsServices = new TagsServices();
            LoadNotes();
        }

        private void LoadNotes()
        {
            
            var notes = noteServices.GetNotesTitlesAndIDs();
            foreach (var note in notes)
            {
                list_NotesPG1.Items.Add(note.Item2);
                list_NotesPG4.Items.Add(note.Item2);
                list_NotesPG4.Items.Add(note.Item2);
                list_NotesPG6.Items.Add(note.Item2);
            }
        }
       /* private void LoadTags()
        {
            List<> tags = tagsServices.GetNoteTags();
            foreach (var tag in tags)
            {
                list_TagsPG3.Items.Add(tags);
                list_TagsPG4.Items.Add(tags);
            }
        }
        private void btn_DeleteNote_Click(object sender, EventArgs e)
        {
            string note = list_NotesPG1.SelectedItem.ToString();
           
        }

        private void btn_ExportNote_Click(object sender, EventArgs e)
        {
            string note = list_NotesPG1.SelectedItem.ToString();
        }

        private void btn_CreateNote_Click(object sender, EventArgs e)
        {
            string noteName = txt_NoteName.Text.Trim();
            string noteDescription = rich_NoteDescriptionPG2.Text;
            noteServices.CreateNote(noteName, noteDescription);
            
        }

        private void btn_CreateTag_Click(object sender, EventArgs e)
        {
            string tagName = txt_TagNamePG3.Text.Trim();
            tagsServices.CreateTag(tagName);
        }

        private void btn_AddTagToNote_Click(object sender, EventArgs e)
        {

        }

        private void btn_UpdateNote_Click(object sender, EventArgs e)
        {
            string noteDescription = rich_NoteDescriptionPG5.Text;
           
        }

        private void btn_SearcNotes_Click(object sender, EventArgs e)
        {
            string tagName = txt_TagNamePG6.Text;
            string noteDescription = rich_NoteDescriptionPG6.Text;

        }

        private void list_NotesPG1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
