import { defineStore } from 'pinia';

class Note {
  constructor(id, name, text, completed=false) {
    this.id = id;
    this.name = name;
    this.text = text;
    this.completed = completed;
  }
}

export const useNoteStore = defineStore('noteStore', {
  state: () => ({
    notes: [
      {
        id: 0,
        name: "note 1",
        completed: false
      },
      {
        id: 1,
        name: "note 2",
        completed: false
      }
    ]
  }),
  actions: {
    getNoteList(id) {
      return this.notes.find(note => note.id == id);
    },
    getNotesById(id) {
      const Notes = this.getNoteList(id);
      return Notes ? Notes.childrenNotesLists : [];
    },
    createNote(name, text) {
      let id = this.notes.length;
      const newNote = new Note(id, name, text);
      this.notes.push(newNote);
      return newNote
    },
    updateNote(note) {
      this.notes[this.notes.findIndex(el => el.id == note.id)] = note;
      console.log(note)
    }
  }
});
