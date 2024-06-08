import { defineStore } from 'pinia';

class NoteList {
  constructor(id, text, Notes = []) {
    this.id = id;
    this.text = name;
    this.Notes = Notes;
  }
}

export const useNotesListStore = defineStore('noteListStore', {
  state: () => ({
    noteList: [
      {
        id: 0,
        name: "note 1",
        notes: [0, 1, 2]
      }
    ]
  }),
  actions: {
    getNoteList(id) {
      return this.noteList.find(folder => folder.id == id);
    },
    getNotesById(id) {
      const Notes = this.getNoteList(id);
      return Notes ? Notes.childrenNotesLists : [];
    },
    createNotesList(id, text, position) {
      const newNoteList = new NoteList(id, text, position);
      this.noteList.push(newNoteList);
      return newNoteList;
    }
  }
});
