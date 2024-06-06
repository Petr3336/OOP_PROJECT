import { defineStore } from 'pinia';

class NoteList {
  constructor(id, text, Notes = []) {
    this.id = id;
    this.text = text;
    this.Notes = Notes;
  }
}

export const useFolderStore = defineStore('noteListStore', {
  state: () => ({
    noteList: []
  }),
  actions: {
    getNoteList(id) {
      return this.noteList.find(folder => folder.id === id);
    },
    getNotesListsById(id) {
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
