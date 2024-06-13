<template>
  <v-container
    fluid
    full-width
    class="d-flex flex-column-reverse justify-end h-100"
  >
    <noteComponent
      v-if="notes == undefined || notes.length == 0"
      class="my-6 mx-16"
    ></noteComponent>
    <noteComponent
      v-else
      v-for="note in notes"
      v-bind:key="note.id"
      :note="note"
      :editing="editing"
      :deleting="deleting"
      class="my-6 mx-16"
    ></noteComponent>
  </v-container>
  <notesToolbar
    :no-notes="notes == undefined || notes.length == 0"
    @edit-mode="
      {
        (editing = !editing), (deleting = false);
      }
    "
    @delete-mode="
      {
        (deleting = !deleting), (editing = false);
      }
    "
  />
</template>
<script>
import { useNotesListStore } from "../stores/NotesListsStore";
import { useNoteStore } from "../stores/NoteStore";
import { storeToRefs } from "pinia";
import noteComponent from "../components/NoteComponent.vue";
import notesToolbar from "../components/NotesToolbar.vue";
export default {
  name: "NoteListView",
  components: {
    noteComponent,
    notesToolbar,
  },
  data() {
    return {
      //noteList: "",
      editing: false,
      deleting: false,
      //noteListId: null,
      notes: [],
    };
  },
  methods: {
    // updateNotesList() {
    //   this.noteList = this.noteListStore.getNoteList(this.$route.params.id);
    // },
  },
  setup() {
    const noteListStore = useNotesListStore();
    const notesStore = useNoteStore();
    const notesRefStore = storeToRefs(notesStore).notes;
    return { noteListStore, notesStore, notesRefStore };
  },
  mounted: async function () {
    // this.updateNotesList();
    //this.noteListId = this.$route.params.id;
    await this.notesStore.refreshNotesFromServer(this.$route.params.id);
    this.notes = await this.notesRefStore.filter(
      (note) => note.noteListId == this.noteListId
    );
  },
  watch: {
    "$route.params.id": {
      async handler(newVal, oldVal) {
        if (newVal !== oldVal) {
          await this.notesStore
            .refreshNotesFromServer(this.$route.params.id)
            .then((res) => {
              this.notes = this.notesRefStore.filter(
                (note) => note.noteListId == res
              );
            });
        }
      },
      immediate: true,
    },
    notesStore: {
      async handler() {
          console.log("a");
          this.notes = await this.notesRefStore.filter(
            (note) => note.noteListId == this.$route.params.id
          );
      },
      deep:true
    },
  },
  computed: {
    noteListId() {
      return this.$route.params.id;
    },
    noteList() {
      return this.noteListStore.getNoteList(this.noteListId);
    },
    // notes() {
    //   if (this.$route.params.id) {
    //     let notes = this.notesRefStore.filter(
    //       (note) => note.noteListId == this.noteListId
    //     );
    //     return notes;
    //   }
    //   return [];
    // },
    // notes() {
    //   if (this.noteList && this.noteList.notes) {
    //     let noteList = this.noteList.notes.map((noteId) =>
    //       this.notesStore.getNoteList(noteId)
    //     );
    //     return noteList;
    //   }
    //   return [];
    // },
  },
};
</script>
<style scoped></style>
