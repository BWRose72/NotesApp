CREATE Database NotesDB;
USE NotesDB;

CREATE TABLE Notes
(
	NoteID INT PRIMARY KEY IDENTITY(1, 1),
	NoteTitle NVARCHAR(100),
	NoteContent NVARCHAR(MAX),
	DateCreated DATE,
	DateUpdated DATE
);

CREATE TABLE Tags
(
	TagID INT PRIMARY KEY IDENTITY(1, 1),
	TagContent NVARCHAR(100)
);

CREATE TABLE NotesTags
(
	NoteID INT FOREIGN KEY REFERENCES Notes(NoteID) ON DELETE CASCADE,
	TagID INT FOREIGN KEY REFERENCES Tags(TagID) ON DELETE CASCADE,
	CONSTRAINT PK_NotesTags PRIMARY KEY (NoteID, TagID)
);

----------------------------------------------------------------------------------------------
--Use this to find the names of the foreign keys
SELECT name 
FROM sys.foreign_keys 
WHERE parent_object_id = OBJECT_ID('NotesTags');


ALTER TABLE NotesTags DROP CONSTRAINT FK__NotesTags__NoteI__5812160E;
ALTER TABLE NotesTags DROP CONSTRAINT FK__NotesTags__TagID__59063A47;

-- Recreate foreign key constraints with ON DELETE CASCADE
ALTER TABLE NotesTags
ADD CONSTRAINT FK_NotesTags_Note
FOREIGN KEY (NoteID) REFERENCES Notes(NoteID) ON DELETE CASCADE;

ALTER TABLE NotesTags
ADD CONSTRAINT FK_NotesTags_Tag
FOREIGN KEY (TagID) REFERENCES Tags(TagID) ON DELETE CASCADE;

-----------------------------------------------------------------
ALTER TABLE Notes
ALTER COLUMN NoteTitle NVARCHAR(100),
ALTER COLUMN NoteContent NVARCHAR(MAX);

ALTER TABLE Tags
ALTER COLUMN TagContent NVARCHAR(100);