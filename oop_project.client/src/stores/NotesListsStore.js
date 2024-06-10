import { defineStore } from "pinia";

class NoteList {
  constructor(id, name, text, Notes = []) {
    this.id = id;
    this.name = name;
    this.text = text;
    this.notes = Notes;
  }
}

export const useNotesListStore = defineStore("noteListStore", {
  state: () => ({
    noteList: [
      {
        id: 0,
        name: "note 1",
        notes: [0, 1],
      },
      {
        id: 1,
        name: "note 2",
        notes: [],
      },
    ],
  }),
  actions: {
    getNoteList(id) {
      return this.noteList.find((folder) => folder.id == id);
    },
    getNotesById(id) {
      const Notes = this.getNoteList(id);
      return Notes ? Notes.childrenNotesLists : [];
    },
    createNotesList(name, text) {
      let id = Math.ceil(Math.random()*1000000);
      const newNoteList = new NoteList(id, name, text);
      this.noteList.push(newNoteList);
      return newNoteList;
    },
    addNote(noteListId, noteId) {
      let pushingIndex = this.noteList.findIndex(
        (noteList) => noteList.id == noteListId
      );
      this.noteList[pushingIndex].notes.push(noteId);
      let updatedNoteList = this.noteList[pushingIndex];
      return updatedNoteList;
    },
  },
});
