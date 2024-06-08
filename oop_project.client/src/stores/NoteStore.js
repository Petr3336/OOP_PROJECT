import { defineStore } from 'pinia';

class Note {
  constructor(id, name, text, Notes = []) {
    this.id = id;
    this.name = name;
    this.text = text;
    this.Notes = Notes;
  }
}

export const useNoteStore = defineStore('noteStore', {
  state: () => ({
    note: [
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
      return this.note.find(folder => folder.id == id);
    },
    getNotesById(id) {
      const Notes = this.getNoteList(id);
      return Notes ? Notes.childrenNotesLists : [];
    },
    createNotesList(id, text, position) {
      const newNoteList = new Note(id, text, position);
      this.noteList.push(newNoteList);
      return newNoteList;
    }
  }
});
