import { defineStore } from "pinia";
import axios from "../axios/AxiosConfiguration";

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
    async refreshNoteListsFromServer() {
      await axios
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
    async createNotesList(name, description) {
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
        })
        .catch((error) => {
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
    async removeNotesList(id) {
      console.log("remove")
      await axios.delete(`/api/NoteList/${id}`).then(() => {
        this.noteList.splice(
          this.noteList.findIndex((List) => List.id == id),
          1
        );
      });
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
