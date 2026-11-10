-- note.sql : the table behind the Notes page. Run it ONCE in the Supabase SQL Editor.
-- It adds one table and changes nothing else.

CREATE TABLE IF NOT EXISTS lending.note (
  note_id     BIGINT        GENERATED ALWAYS AS IDENTITY,
  title       VARCHAR(80)   NOT NULL,
  body        VARCHAR(500),
  created_on  DATE          NOT NULL DEFAULT CURRENT_DATE,
  CONSTRAINT pk_note PRIMARY KEY (note_id)
);

INSERT INTO lending.note (title, body) VALUES
  ('Welcome', 'Notes are reminders for the library staff.'),
  ('Overdue books', 'Call members whose loans show Overdue.');
