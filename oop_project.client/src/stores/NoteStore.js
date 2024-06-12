import { defineStore } from 'pinia';
import axios from "axios";
axios.defaults.headers.common = {
  Authorization: `Bearer ${localStorage.getItem("accessToken")}`,
};

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
    ]
  }),
  actions: {
    refreshNotesFromServer() {
      axios
        .get("/api/NoteList")
        .then((response) => (this.noteList = response.data));
    },
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
