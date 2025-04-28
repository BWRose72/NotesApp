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
            LoadTags();
        }

        private void LoadNotes()
        {

            var notes = noteServices.GetNotesTitlesAndIDs();
            foreach (var note in notes)
            {
                list_NotesPG1.Items.Add(note.Item1 + ": " + note.Item2);

                list_NotesPG4.Items.Add(note.Item1 + ": " + note.Item2);
                list_NotesPG5.Items.Add(note.Item1 + ": " + note.Item2);
                list_NotesPG6.Items.Add(note.Item1 + ": " + note.Item2);
            }
        }
        private void LoadTags()
        {
            List<string> tags = tagsServices.GetAllTags();
            foreach (var tag in tags)
            {
                list_TagsPG3.Items.Add(tags);
                list_TagsPG4.Items.Add(tags);
            }
        }

        private void btn_DeleteNote_Click(object sender, EventArgs e)
        {
            if (list_NotesPG1.SelectedItem == null)
            {
                MessageBox.Show("Select note!");
                return;
            }
            string note = list_NotesPG1.SelectedItem.ToString();
            string[] noteInfo = note.Split(": ");
            int noteId = int.Parse(noteInfo[0]);

            var confirmResult = MessageBox.Show("Are you sure you want to delete this note?",
                "Confirm Delete", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.No)
            {
                return;
            }

            noteServices.DeleteNote(noteId);

            list_NotesPG1.Items.Remove(note);
            list_NotesPG4.Items.Remove(note);
            list_NotesPG5.Items.Remove(note);
            list_NotesPG6.Items.Remove(note);

            MessageBox.Show("The note is deleted!");
        }

        private void btn_ExportNote_Click(object sender, EventArgs e)
        {
            string note = list_NotesPG1.SelectedItem.ToString();

        }

        private void btn_CreateNote_Click(object sender, EventArgs e)
        {
            if (txt_TagNamePG3.Text.Trim() == null || rich_NoteDescriptionPG2.Text == null)
            {
                MessageBox.Show("Error! Please add valid note and description!");
            }
            string noteName = txt_NoteName.Text.Trim();
            string noteDescription = rich_NoteDescriptionPG2.Text;

            noteServices.CreateNote(noteName, noteDescription);
            MessageBox.Show("The note is created");
        }

        private void btn_CreateTag_Click(object sender, EventArgs e)
        {
            if (txt_TagNamePG3.Text.Trim() == null)
            {
                MessageBox.Show("Error! Please add valid tag!");
            }
            string tagName = txt_TagNamePG3.Text.Trim();
            tagsServices.CreateTag(tagName);
            MessageBox.Show("The tag is created!");
        }

        private void btn_AddTagToNote_Click(object sender, EventArgs e)
        {
            if (list_NotesPG4.SelectedItem == null || list_TagsPG4.SelectedItem == null)
            {
                MessageBox.Show("You must select tag and note!");
            }
            string note = list_NotesPG4.SelectedItem.ToString();
            List<string> selectedTag = list_TagsPG4.SelectedItem.ToString().Split(' ').ToList();
            int tagId = int.Parse(selectedTag[0]);
            try
            {
                tagsServices.AddTagToNote(tagId, note);
                MessageBox.Show("The tag is added to the note.");
            }
            catch
            {
                MessageBox.Show("The tag is already added.");
            }
        }

        private void btn_UpdateNote_Click(object sender, EventArgs e)
        {
            string note = list_NotesPG5.SelectedItem.ToString();
            string[] noteInfo = note.Split(": ");
            int noteId = int.Parse(noteInfo[0]);
            var noteDescription = rich_NoteDescriptionPG5.Text;
            noteServices.UpdateNoteContents(noteId, noteDescription);

        }

        private void btn_SearcNotes_Click(object sender, EventArgs e)
        {

            if (txt_TagNamePG6.Text == null)
            {
                MessageBox.Show("Invalid tag!");
                return;
            }
            string tagName = txt_TagNamePG6.Text.Trim();
            noteServices.GetFilteredNotes(tagName);

        }

        private void list_NotesPG1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_DeleteTags_Click(object sender, EventArgs e)
        {
            //delete tags
        }
    }
}
