using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using BusinessLogicLayer;

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

            list_NotesPG1.Items.Clear();
            list_NotesPG4.Items.Clear();
            list_NotesPG5.Items.Clear();
            list_NotesPG6.Items.Clear();

            foreach (var note in notes)
            {
                string item = note.Item1 + ": " + note.Item2;
                list_NotesPG1.Items.Add(item);
                list_NotesPG4.Items.Add(item);
                list_NotesPG5.Items.Add(item);
                list_NotesPG6.Items.Add(item);
            }
        }

        private void LoadTags()
        {
            List<string> tags = tagsServices.GetAllTags();

            list_TagsPG3.Items.Clear();
            list_TagsPG4.Items.Clear();
            cmbBox_PG6.Items.Clear();

            list_TagsPG3.Items.AddRange(tags.ToArray());
            list_TagsPG4.Items.AddRange(tags.ToArray());
            cmbBox_PG6.Items.AddRange(tagsServices.GetAllTags().ToArray());
        }

        private void btn_DeleteNote_Click(object sender, EventArgs e)
        {
            if (list_NotesPG1.SelectedItem == null)
            {
                MessageBox.Show("Моля изберете бележка!");
                return;
            }
            string note = list_NotesPG1.SelectedItem.ToString();
            list_NotesPG1.ClearSelected();
            string[] noteInfo = note.Split(": ");
            int noteId = int.Parse(noteInfo[0]);

            var confirmResult = MessageBox.Show("Сигурни ли сте, че искате да изтриете тази бележка?",
                "Потвърдете изтриването", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.No)
            {
                return;
            }

            noteServices.DeleteNote(noteId);

            list_NotesPG1.Items.Remove(note);
            list_NotesPG4.Items.Remove(note);
            list_NotesPG5.Items.Remove(note);
            list_NotesPG6.Items.Remove(note);

            MessageBox.Show("Бележката е изтрита!");
        }

        private void btn_ExportNote_Click(object sender, EventArgs e)
        {
            if (list_NotesPG1.SelectedItem == null)
            {
                MessageBox.Show("Моля изберете бележка!");
                return;
            }

            string note = list_NotesPG1.SelectedItem.ToString();
            string[] noteInfo = note.Split(": ");
            int noteId = int.Parse(noteInfo[0]);

            string noteContent = noteServices.GetNoteContents(noteId);
            string fileName = noteInfo[1];

            var folderDialog = new FolderBrowserDialog();
            folderDialog.ShowDialog();
            string folderPath = folderDialog.SelectedPath;

            string filePath = Path.Combine(folderPath, fileName + ".txt");
            FileSaver.SaveFile(filePath, fileName, noteContent, tagsServices.GetNoteTags(noteId).ToArray());
            MessageBox.Show("Файлът е запазен!");
        }

        private void btn_CreateNote_Click(object sender, EventArgs e)
        {
            if (txt_TagNamePG3.Text.Trim() == null)
            {
                MessageBox.Show("Моля въведете текст в полето за заглавие!");
                return;
            }

            if (rich_NoteDescriptionPG2.Text == null)
            {
                MessageBox.Show("Моля въведете текст в полето за съдържание!");
                return;
            }

            string noteName = txt_NoteName.Text.Trim();
            string noteDescription = rich_NoteDescriptionPG2.Text;

            noteServices.CreateNote(noteName, noteDescription);


            LoadNotes();

            txt_NoteName.Clear();
            rich_NoteDescriptionPG2.Clear();

            MessageBox.Show("Бележката е създадена.");
        }

        private void btn_CreateTag_Click(object sender, EventArgs e)
        {
            if (txt_TagNamePG3.Text.Trim() == null)
            {
                MessageBox.Show("Моля въведете текст!");
                return;
            }
            string tagName = txt_TagNamePG3.Text.Trim();
            tagsServices.CreateTag(tagName);

            LoadTags();
            txt_TagNamePG3.Clear();

            //MessageBox.Show("Етикетът е създаден!");
        }

        private void btn_AddTagToNote_Click(object sender, EventArgs e)//maybe ready
        {
            if (list_NotesPG4.SelectedItem == null || list_TagsPG4.SelectedItem == null)
            {
                MessageBox.Show("Моля изберете бележка и етикет!");
                return;
            }
            string note = list_NotesPG4.SelectedItem.ToString();
            string[] noteInfo = note.Split(": ");
            int noteId = int.Parse(noteInfo[0]);

            List<string> selectedTag = list_TagsPG4.SelectedItem.ToString().Split(' ').ToList();
            int tagId = tagsServices.GetTagIDFromContent(list_TagsPG4.SelectedItem.ToString());

            try
            {
                tagsServices.AddTagToNote(noteId, tagId);
                MessageBox.Show("Етикетът е добавен към бележката.");
            }
            catch (Exception)
            {
                MessageBox.Show("Този етикет вече е добавен към бележката.");
                throw;
            }
        }

        private void btn_UpdateNote_Click(object sender, EventArgs e)
        {
            if (list_NotesPG5.SelectedItem == null)
            {
                MessageBox.Show("Моля изберете бележка!");
                return;
            }
            string note = list_NotesPG5.SelectedItem.ToString();
            string[] noteInfo = note.Split(": ");
            int noteId = int.Parse(noteInfo[0]);
            var noteDescription = rich_NoteDescriptionPG5.Text;
            noteServices.UpdateNoteContents(noteId, noteDescription);
            MessageBox.Show("Бележката е променена!");
        }

        private void btn_SearcNotes_Click(object sender, EventArgs e)
        {

            if (cmbBox_PG6.SelectedItem == null)
            {
                MessageBox.Show("Моля изберете етикет!");
                return;
            }

            list_NotesPG6.Items.Clear();
            foreach (var note in noteServices.GetFilteredNotes(cmbBox_PG6.SelectedItem.ToString()))
            {
                list_NotesPG6.Items.Add(note.Item1 + ": " + note.Item2);
            }
        }

        private void list_NotesPG1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_NotesPG1.SelectedItem == null)
            {
                return;
            }
            string note = list_NotesPG1.SelectedItem.ToString();
            string[] noteInfo = note.Split(": ");
            int noteId = int.Parse(noteInfo[0]);
            rich_NoteContentPG1.Text = noteServices.GetNoteContents(noteId);
        }

        private void btn_DeleteTags_Click(object sender, EventArgs e)
        {
            if (list_TagsPG3.SelectedItem == null)
            {
                MessageBox.Show("Моля изберете етикет!");
                return;
            }
            string tag = list_TagsPG3.SelectedItem.ToString();
            int tagId = tagsServices.GetTagIDFromContent(tag);

            var confirmResult = MessageBox.Show("Сигурни ли сте, че искате да изтриете този етикет?",
                "Потърдете изтриването", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.No)
            {
                return;
            }

            tagsServices.DeleteTag(tagId);

            list_TagsPG3.Items.Remove(tag);
            list_TagsPG4.Items.Remove(tag);

            //MessageBox.Show("Етикетът е изтрит!");
        }

        private void list_NotesPG5_SelectedIndexChanged(object sender, EventArgs e)
        {
            string note = list_NotesPG5.SelectedItem.ToString();
            string[] noteInfo = note.Split(": ");
            int noteId = int.Parse(noteInfo[0]);
            label12.Text = "Етикети: " + string.Join(", ", tagsServices.GetNoteTags(noteId));
            rich_NoteDescriptionPG5.Text = noteServices.GetNoteContents(noteId);
        }

        private void rich_NoteDescriptionPG6_TextChanged(object sender, EventArgs e)
        {
            string note = list_NotesPG6.SelectedItem.ToString();
            string[] noteInfo = note.Split(": ");
            int noteId = int.Parse(noteInfo[0]);
            rich_NoteDescriptionPG6.Text = noteServices.GetNoteContents(noteId);
        }

        private void list_NotesPG6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_NotesPG6.SelectedItem == null)
            {
                return;
            }
            string note = list_NotesPG6.SelectedItem.ToString();
            string[] noteInfo = note.Split(": ");
            int noteId = int.Parse(noteInfo[0]);
            rich_NoteDescriptionPG6.Text = noteServices.GetNoteContents(noteId);
        }
    }
}
