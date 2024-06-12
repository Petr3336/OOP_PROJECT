import { defineStore } from "pinia";
import axios from "axios";
axios.defaults.headers.common = {
  Authorization: `Bearer ${localStorage.getItem("accessToken")}`,
};

class NoteList {
  constructor(id, name, description, Notes = []) {
    this.id = id;
    this.name = name;
    this.description = description;
    this.notes = Notes;
  }
}

export const useNotesListStore = defineStore("noteListStore", {
  state: () => ({
    noteList: [],
  }),
  actions: {
    refreshNoteListsFromServer() {
      axios
        .get("/api/NoteList")
        .then((response) => (this.noteList = response.data));
    },
    getNoteList(id) {
      return this.noteList.find((folder) => folder.id == id);
    },
    getNotesById(id) {
      const Notes = this.getNoteList(id);
      return Notes ? Notes.childrenNotesLists : [];
    },
    createNotesList(name, description) {
      axios
        .post("/api/NoteList", { name, description, position: 0 })
        .then((response) => {
          this.noteList.push(
            new NoteList(
              response.data.id,
              response.data.name,
              response.data.description
            )
          );
        }).catch((error) => {
          alert(
            error.response.data.errors[
              Object.keys(error.response.data.errors)[0]
            ]
          );
          throw "noteListCreationError";
        });
      //const newNoteList = new NoteList(id, name, description);
      //this.noteList.push(newNoteList);
      //return newNoteList;
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
