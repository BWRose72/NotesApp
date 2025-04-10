namespace NotesApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            page6_SearchNotes = new TabPage();
            list_NotesPG6 = new ListBox();
            label17 = new Label();
            rich_NoteDescriptionPG6 = new RichTextBox();
            txt_TagNamePG6 = new TextBox();
            btn_SearcNotes = new Button();
            label16 = new Label();
            label15 = new Label();
            page5_UpdateNote = new TabPage();
            lbl_TagName = new Label();
            label13 = new Label();
            rich_NoteDescriptionPG5 = new RichTextBox();
            btn_UpdateNote = new Button();
            label12 = new Label();
            label10 = new Label();
            list_NotesPG5 = new ListBox();
            page4_AddTagToNote = new TabPage();
            list_NotesPG4 = new ListBox();
            btn_AddTagToNote = new Button();
            label8 = new Label();
            label7 = new Label();
            list_TagsPG4 = new ListBox();
            page3_CreateTag = new TabPage();
            label6 = new Label();
            list_TagsPG3 = new ListBox();
            btn_CreateTag = new Button();
            txt_TagNamePG3 = new TextBox();
            label5 = new Label();
            page2_CreateNote = new TabPage();
            btn_CreateNote = new Button();
            rich_NoteDescriptionPG2 = new RichTextBox();
            txt_NoteName = new TextBox();
            label4 = new Label();
            label2 = new Label();
            page1_Notes = new TabPage();
            btn_DeleteNote = new Button();
            btn_ExportNote = new Button();
            lst_Notes = new ListBox();
            list_NotesPG1 = new ListBox();
            label1 = new Label();
            tabControl1 = new TabControl();
            page6_SearchNotes.SuspendLayout();
            page5_UpdateNote.SuspendLayout();
            page4_AddTagToNote.SuspendLayout();
            page3_CreateTag.SuspendLayout();
            page2_CreateNote.SuspendLayout();
            page1_Notes.SuspendLayout();
            tabControl1.SuspendLayout();
            SuspendLayout();
            // 
            // page6_SearchNotes
            // 
            page6_SearchNotes.BackColor = Color.Wheat;
            page6_SearchNotes.Controls.Add(list_NotesPG6);
            page6_SearchNotes.Controls.Add(label17);
            page6_SearchNotes.Controls.Add(rich_NoteDescriptionPG6);
            page6_SearchNotes.Controls.Add(txt_TagNamePG6);
            page6_SearchNotes.Controls.Add(btn_SearcNotes);
            page6_SearchNotes.Controls.Add(label16);
            page6_SearchNotes.Controls.Add(label15);
            page6_SearchNotes.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            page6_SearchNotes.ForeColor = Color.SaddleBrown;
            page6_SearchNotes.Location = new Point(4, 29);
            page6_SearchNotes.Name = "page6_SearchNotes";
            page6_SearchNotes.Padding = new Padding(3);
            page6_SearchNotes.Size = new Size(1130, 604);
            page6_SearchNotes.TabIndex = 6;
            page6_SearchNotes.Text = "Търсене по етикет";
            // 
            // list_NotesPG6
            // 
            list_NotesPG6.BackColor = Color.AntiqueWhite;
            list_NotesPG6.ForeColor = Color.SaddleBrown;
            list_NotesPG6.FormattingEnabled = true;
            list_NotesPG6.ItemHeight = 28;
            list_NotesPG6.Location = new Point(43, 155);
            list_NotesPG6.Name = "list_NotesPG6";
            list_NotesPG6.Size = new Size(359, 424);
            list_NotesPG6.TabIndex = 6;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(452, 116);
            label17.Name = "label17";
            label17.Size = new Size(143, 28);
            label17.TabIndex = 5;
            label17.Text = "Съдържание:";
            // 
            // rich_NoteDescriptionPG6
            // 
            rich_NoteDescriptionPG6.ForeColor = Color.SaddleBrown;
            rich_NoteDescriptionPG6.Location = new Point(452, 155);
            rich_NoteDescriptionPG6.Name = "rich_NoteDescriptionPG6";
            rich_NoteDescriptionPG6.Size = new Size(631, 424);
            rich_NoteDescriptionPG6.TabIndex = 4;
            rich_NoteDescriptionPG6.Text = "";
            // 
            // txt_TagNamePG6
            // 
            txt_TagNamePG6.BackColor = Color.AntiqueWhite;
            txt_TagNamePG6.Location = new Point(147, 55);
            txt_TagNamePG6.Name = "txt_TagNamePG6";
            txt_TagNamePG6.Size = new Size(448, 34);
            txt_TagNamePG6.TabIndex = 2;
            // 
            // btn_SearcNotes
            // 
            btn_SearcNotes.BackColor = Color.AntiqueWhite;
            btn_SearcNotes.Location = new Point(644, 32);
            btn_SearcNotes.Name = "btn_SearcNotes";
            btn_SearcNotes.Size = new Size(439, 81);
            btn_SearcNotes.TabIndex = 3;
            btn_SearcNotes.Text = "Търсене на бележки";
            btn_SearcNotes.UseVisualStyleBackColor = false;
            btn_SearcNotes.Click += btn_SearcNotes_Click;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(43, 116);
            label16.Name = "label16";
            label16.Size = new Size(105, 28);
            label16.TabIndex = 1;
            label16.Text = "Бележки:";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(43, 58);
            label15.Name = "label15";
            label15.Size = new Size(81, 28);
            label15.TabIndex = 0;
            label15.Text = "Етикет:";
            // 
            // page5_UpdateNote
            // 
            page5_UpdateNote.BackColor = Color.Wheat;
            page5_UpdateNote.Controls.Add(lbl_TagName);
            page5_UpdateNote.Controls.Add(label13);
            page5_UpdateNote.Controls.Add(rich_NoteDescriptionPG5);
            page5_UpdateNote.Controls.Add(btn_UpdateNote);
            page5_UpdateNote.Controls.Add(label12);
            page5_UpdateNote.Controls.Add(label10);
            page5_UpdateNote.Controls.Add(list_NotesPG5);
            page5_UpdateNote.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            page5_UpdateNote.ForeColor = Color.SaddleBrown;
            page5_UpdateNote.Location = new Point(4, 29);
            page5_UpdateNote.Name = "page5_UpdateNote";
            page5_UpdateNote.Padding = new Padding(3);
            page5_UpdateNote.Size = new Size(1130, 604);
            page5_UpdateNote.TabIndex = 5;
            page5_UpdateNote.Text = "Промяна на бележки";
            // 
            // lbl_TagName
            // 
            lbl_TagName.AutoSize = true;
            lbl_TagName.Location = new Point(549, 509);
            lbl_TagName.Name = "lbl_TagName";
            lbl_TagName.Size = new Size(82, 28);
            lbl_TagName.TabIndex = 10;
            lbl_TagName.Text = "label14";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(449, 22);
            label13.Name = "label13";
            label13.Size = new Size(263, 28);
            label13.TabIndex = 9;
            label13.Text = "Съдържание на бележка:";
            // 
            // rich_NoteDescriptionPG5
            // 
            rich_NoteDescriptionPG5.ForeColor = Color.SaddleBrown;
            rich_NoteDescriptionPG5.Location = new Point(449, 53);
            rich_NoteDescriptionPG5.Name = "rich_NoteDescriptionPG5";
            rich_NoteDescriptionPG5.Size = new Size(648, 409);
            rich_NoteDescriptionPG5.TabIndex = 4;
            rich_NoteDescriptionPG5.Text = "";
            // 
            // btn_UpdateNote
            // 
            btn_UpdateNote.BackColor = Color.AntiqueWhite;
            btn_UpdateNote.Location = new Point(914, 486);
            btn_UpdateNote.Name = "btn_UpdateNote";
            btn_UpdateNote.Size = new Size(183, 75);
            btn_UpdateNote.TabIndex = 6;
            btn_UpdateNote.Text = "Променете бележката";
            btn_UpdateNote.UseVisualStyleBackColor = false;
            btn_UpdateNote.Click += btn_UpdateNote_Click;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(449, 509);
            label12.Name = "label12";
            label12.Size = new Size(94, 28);
            label12.TabIndex = 3;
            label12.Text = "Етикети:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(27, 22);
            label10.Name = "label10";
            label10.Size = new Size(197, 28);
            label10.TabIndex = 1;
            label10.Text = "Списък с бележки:";
            // 
            // list_NotesPG5
            // 
            list_NotesPG5.BackColor = Color.AntiqueWhite;
            list_NotesPG5.ForeColor = Color.SaddleBrown;
            list_NotesPG5.FormattingEnabled = true;
            list_NotesPG5.ItemHeight = 28;
            list_NotesPG5.Location = new Point(27, 53);
            list_NotesPG5.Name = "list_NotesPG5";
            list_NotesPG5.Size = new Size(371, 508);
            list_NotesPG5.TabIndex = 0;
            // 
            // page4_AddTagToNote
            // 
            page4_AddTagToNote.BackColor = Color.Wheat;
            page4_AddTagToNote.Controls.Add(list_NotesPG4);
            page4_AddTagToNote.Controls.Add(btn_AddTagToNote);
            page4_AddTagToNote.Controls.Add(label8);
            page4_AddTagToNote.Controls.Add(label7);
            page4_AddTagToNote.Controls.Add(list_TagsPG4);
            page4_AddTagToNote.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            page4_AddTagToNote.Location = new Point(4, 29);
            page4_AddTagToNote.Name = "page4_AddTagToNote";
            page4_AddTagToNote.Padding = new Padding(3);
            page4_AddTagToNote.Size = new Size(1130, 604);
            page4_AddTagToNote.TabIndex = 3;
            page4_AddTagToNote.Text = "Добавяне на етикет към бележка";
            // 
            // list_NotesPG4
            // 
            list_NotesPG4.BackColor = Color.AntiqueWhite;
            list_NotesPG4.ForeColor = Color.SaddleBrown;
            list_NotesPG4.FormattingEnabled = true;
            list_NotesPG4.ItemHeight = 28;
            list_NotesPG4.Location = new Point(669, 57);
            list_NotesPG4.Name = "list_NotesPG4";
            list_NotesPG4.Size = new Size(447, 508);
            list_NotesPG4.TabIndex = 6;
            // 
            // btn_AddTagToNote
            // 
            btn_AddTagToNote.BackColor = Color.AntiqueWhite;
            btn_AddTagToNote.ForeColor = Color.SaddleBrown;
            btn_AddTagToNote.Location = new Point(491, 209);
            btn_AddTagToNote.Name = "btn_AddTagToNote";
            btn_AddTagToNote.Size = new Size(147, 233);
            btn_AddTagToNote.TabIndex = 5;
            btn_AddTagToNote.Text = "Добави етикет към бележка";
            btn_AddTagToNote.UseVisualStyleBackColor = false;
            btn_AddTagToNote.Click += btn_AddTagToNote_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = Color.SaddleBrown;
            label8.Location = new Point(670, 20);
            label8.Name = "label8";
            label8.Size = new Size(197, 28);
            label8.TabIndex = 3;
            label8.Text = "Списък с бележки:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.ForeColor = Color.SaddleBrown;
            label7.Location = new Point(13, 20);
            label7.Name = "label7";
            label7.Size = new Size(187, 28);
            label7.TabIndex = 2;
            label7.Text = "Списък с етикети:";
            // 
            // list_TagsPG4
            // 
            list_TagsPG4.BackColor = Color.AntiqueWhite;
            list_TagsPG4.ForeColor = Color.SaddleBrown;
            list_TagsPG4.FormattingEnabled = true;
            list_TagsPG4.ItemHeight = 28;
            list_TagsPG4.Location = new Point(13, 57);
            list_TagsPG4.Name = "list_TagsPG4";
            list_TagsPG4.Size = new Size(447, 508);
            list_TagsPG4.TabIndex = 0;
            // 
            // page3_CreateTag
            // 
            page3_CreateTag.BackColor = Color.Wheat;
            page3_CreateTag.Controls.Add(label6);
            page3_CreateTag.Controls.Add(list_TagsPG3);
            page3_CreateTag.Controls.Add(btn_CreateTag);
            page3_CreateTag.Controls.Add(txt_TagNamePG3);
            page3_CreateTag.Controls.Add(label5);
            page3_CreateTag.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            page3_CreateTag.ForeColor = Color.SaddleBrown;
            page3_CreateTag.Location = new Point(4, 29);
            page3_CreateTag.Name = "page3_CreateTag";
            page3_CreateTag.Padding = new Padding(3);
            page3_CreateTag.Size = new Size(1130, 604);
            page3_CreateTag.TabIndex = 2;
            page3_CreateTag.Text = "Създаване на етикет";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(496, 29);
            label6.Name = "label6";
            label6.Size = new Size(187, 28);
            label6.TabIndex = 4;
            label6.Text = "Списък с етикети:";
            // 
            // list_TagsPG3
            // 
            list_TagsPG3.BackColor = Color.AntiqueWhite;
            list_TagsPG3.ForeColor = Color.SaddleBrown;
            list_TagsPG3.FormattingEnabled = true;
            list_TagsPG3.ItemHeight = 28;
            list_TagsPG3.Location = new Point(496, 71);
            list_TagsPG3.Name = "list_TagsPG3";
            list_TagsPG3.Size = new Size(580, 480);
            list_TagsPG3.TabIndex = 3;
            // 
            // btn_CreateTag
            // 
            btn_CreateTag.BackColor = Color.AntiqueWhite;
            btn_CreateTag.Location = new Point(41, 304);
            btn_CreateTag.Name = "btn_CreateTag";
            btn_CreateTag.Size = new Size(397, 147);
            btn_CreateTag.TabIndex = 2;
            btn_CreateTag.Text = "Създай етикет";
            btn_CreateTag.UseVisualStyleBackColor = false;
            btn_CreateTag.Click += btn_CreateTag_Click;
            // 
            // txt_TagNamePG3
            // 
            txt_TagNamePG3.BackColor = Color.AntiqueWhite;
            txt_TagNamePG3.ForeColor = Color.SaddleBrown;
            txt_TagNamePG3.Location = new Point(41, 167);
            txt_TagNamePG3.Name = "txt_TagNamePG3";
            txt_TagNamePG3.Size = new Size(397, 34);
            txt_TagNamePG3.TabIndex = 1;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(41, 108);
            label5.Name = "label5";
            label5.Size = new Size(170, 28);
            label5.TabIndex = 0;
            label5.Text = "Име на етикета:";
            // 
            // page2_CreateNote
            // 
            page2_CreateNote.BackColor = Color.Wheat;
            page2_CreateNote.Controls.Add(btn_CreateNote);
            page2_CreateNote.Controls.Add(rich_NoteDescriptionPG2);
            page2_CreateNote.Controls.Add(txt_NoteName);
            page2_CreateNote.Controls.Add(label4);
            page2_CreateNote.Controls.Add(label2);
            page2_CreateNote.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            page2_CreateNote.Location = new Point(4, 29);
            page2_CreateNote.Name = "page2_CreateNote";
            page2_CreateNote.Padding = new Padding(3);
            page2_CreateNote.Size = new Size(1130, 604);
            page2_CreateNote.TabIndex = 1;
            page2_CreateNote.Text = "Създаване на бележка";
            // 
            // btn_CreateNote
            // 
            btn_CreateNote.BackColor = Color.AntiqueWhite;
            btn_CreateNote.ForeColor = Color.SaddleBrown;
            btn_CreateNote.Location = new Point(924, 261);
            btn_CreateNote.Name = "btn_CreateNote";
            btn_CreateNote.Size = new Size(152, 204);
            btn_CreateNote.TabIndex = 6;
            btn_CreateNote.Text = "Създай бележка";
            btn_CreateNote.UseVisualStyleBackColor = false;
            btn_CreateNote.Click += btn_CreateNote_Click;
            // 
            // rich_NoteDescriptionPG2
            // 
            rich_NoteDescriptionPG2.ForeColor = Color.SaddleBrown;
            rich_NoteDescriptionPG2.Location = new Point(48, 163);
            rich_NoteDescriptionPG2.Name = "rich_NoteDescriptionPG2";
            rich_NoteDescriptionPG2.Size = new Size(849, 400);
            rich_NoteDescriptionPG2.TabIndex = 5;
            rich_NoteDescriptionPG2.Text = "";
            // 
            // txt_NoteName
            // 
            txt_NoteName.BackColor = Color.AntiqueWhite;
            txt_NoteName.ForeColor = Color.SaddleBrown;
            txt_NoteName.Location = new Point(200, 42);
            txt_NoteName.Name = "txt_NoteName";
            txt_NoteName.Size = new Size(867, 34);
            txt_NoteName.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.ForeColor = Color.SaddleBrown;
            label4.Location = new Point(48, 124);
            label4.Name = "label4";
            label4.Size = new Size(143, 28);
            label4.TabIndex = 2;
            label4.Text = "Съдържание:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.SaddleBrown;
            label2.Location = new Point(48, 48);
            label2.Name = "label2";
            label2.Size = new Size(105, 28);
            label2.TabIndex = 0;
            label2.Text = "Заглавие:";
            // 
            // page1_Notes
            // 
            page1_Notes.BackColor = Color.Wheat;
            page1_Notes.Controls.Add(btn_DeleteNote);
            page1_Notes.Controls.Add(btn_ExportNote);
            page1_Notes.Controls.Add(lst_Notes);
            page1_Notes.Controls.Add(list_NotesPG1);
            page1_Notes.Controls.Add(label1);
            page1_Notes.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            page1_Notes.Location = new Point(4, 29);
            page1_Notes.Name = "page1_Notes";
            page1_Notes.Padding = new Padding(3);
            page1_Notes.Size = new Size(1130, 604);
            page1_Notes.TabIndex = 0;
            page1_Notes.Text = "Бележки";
            // 
            // btn_DeleteNote
            // 
            btn_DeleteNote.BackColor = Color.AntiqueWhite;
            btn_DeleteNote.ForeColor = Color.SaddleBrown;
            btn_DeleteNote.Location = new Point(453, 121);
            btn_DeleteNote.Name = "btn_DeleteNote";
            btn_DeleteNote.Size = new Size(226, 147);
            btn_DeleteNote.TabIndex = 5;
            btn_DeleteNote.Text = "Изтриване на бележки";
            btn_DeleteNote.UseVisualStyleBackColor = false;
            btn_DeleteNote.Click += btn_DeleteNote_Click;
            // 
            // btn_ExportNote
            // 
            btn_ExportNote.BackColor = Color.AntiqueWhite;
            btn_ExportNote.ForeColor = Color.SaddleBrown;
            btn_ExportNote.Location = new Point(453, 361);
            btn_ExportNote.Name = "btn_ExportNote";
            btn_ExportNote.Size = new Size(226, 147);
            btn_ExportNote.TabIndex = 4;
            btn_ExportNote.Text = "Експортиране на бележки";
            btn_ExportNote.UseVisualStyleBackColor = false;
            btn_ExportNote.Click += btn_ExportNote_Click;
            // 
            // lst_Notes
            // 
            lst_Notes.BackColor = Color.AntiqueWhite;
            lst_Notes.ForeColor = Color.SaddleBrown;
            lst_Notes.FormattingEnabled = true;
            lst_Notes.ItemHeight = 28;
            lst_Notes.Location = new Point(723, 61);
            lst_Notes.Name = "lst_Notes";
            lst_Notes.Size = new Size(395, 508);
            lst_Notes.TabIndex = 3;
            // 
            // list_NotesPG1
            // 
            list_NotesPG1.BackColor = Color.AntiqueWhite;
            list_NotesPG1.ForeColor = Color.SaddleBrown;
            list_NotesPG1.FormattingEnabled = true;
            list_NotesPG1.ItemHeight = 28;
            list_NotesPG1.Location = new Point(13, 61);
            list_NotesPG1.Name = "list_NotesPG1";
            list_NotesPG1.Size = new Size(395, 508);
            list_NotesPG1.TabIndex = 1;
            list_NotesPG1.SelectedIndexChanged += list_NotesPG1_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.SaddleBrown;
            label1.Location = new Point(13, 20);
            label1.Name = "label1";
            label1.Size = new Size(197, 28);
            label1.TabIndex = 0;
            label1.Text = "Списък с бележки:";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(page1_Notes);
            tabControl1.Controls.Add(page2_CreateNote);
            tabControl1.Controls.Add(page3_CreateTag);
            tabControl1.Controls.Add(page4_AddTagToNote);
            tabControl1.Controls.Add(page5_UpdateNote);
            tabControl1.Controls.Add(page6_SearchNotes);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1138, 637);
            tabControl1.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1138, 637);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "Notes App";
            page6_SearchNotes.ResumeLayout(false);
            page6_SearchNotes.PerformLayout();
            page5_UpdateNote.ResumeLayout(false);
            page5_UpdateNote.PerformLayout();
            page4_AddTagToNote.ResumeLayout(false);
            page4_AddTagToNote.PerformLayout();
            page3_CreateTag.ResumeLayout(false);
            page3_CreateTag.PerformLayout();
            page2_CreateNote.ResumeLayout(false);
            page2_CreateNote.PerformLayout();
            page1_Notes.ResumeLayout(false);
            page1_Notes.PerformLayout();
            tabControl1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabPage page6_SearchNotes;
        private ListBox list_NotesPG6;
        private Label label17;
        private RichTextBox rich_NoteDescriptionPG6;
        private TextBox txt_TagNamePG6;
        private Button btn_SearcNotes;
        private Label label16;
        private Label label15;
        private TabPage page5_UpdateNote;
        private Label lbl_TagName;
        private Label label13;
        private RichTextBox rich_NoteDescriptionPG5;
        private Button btn_UpdateNote;
        private Label label12;
        private Label label10;
        private ListBox list_NotesPG5;
        private TabPage page4_AddTagToNote;
        private ListBox list_NotesPG4;
        private Button btn_AddTagToNote;
        private Label label8;
        private Label label7;
        private ListBox list_TagsPG4;
        private TabPage page3_CreateTag;
        private Label label6;
        private ListBox list_TagsPG3;
        private Button btn_CreateTag;
        private TextBox txt_TagNamePG3;
        private Label label5;
        private TabPage page2_CreateNote;
        private TextBox txt_NoteName;
        private Label label4;
        private Label label2;
        private TabPage page1_Notes;
        private Button btn_ExportNote;
        private ListBox lst_Notes;
        private ListBox list_NotesPG1;
        private Label label1;
        private TabControl tabControl1;
        private Button btn_DeleteNote;
        private Button btn_CreateNote;
        private RichTextBox rich_NoteDescriptionPG2;
    }
}
