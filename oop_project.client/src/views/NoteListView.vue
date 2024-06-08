<template>
  <v-container fluid full-width class="d-flex flex-column">
    <noteComponent
      v-if="noteList == undefined"
      class="ma-6"
    ></noteComponent>
    <noteComponent
      v-else
      v-for="note in notes"
      v-bind:key="note.id"
      :note="note"
      class="ma-6"
    ></noteComponent>
  </v-container>
  <notesToolbar />
</template>
<script>
import { useNotesListStore } from "../stores/NotesListsStore";
import { useNoteStore } from "../stores/NoteStore";
import noteComponent from "../components/NoteComponent.vue";
import notesToolbar from "../components/NotesToolbar.vue";
export default {
  name: "NoteListView",
  components: {
    noteComponent,
    notesToolbar
  },
  data() {
    return {
      noteList: "",
      notes: [],
    };
  },
  methods: {
    updateNotes() {
      this.noteList = this.noteListStore.getNoteList(this.$route.params.id);
      if (this.noteList && this.noteList.notes) {
        this.notes = this.noteList.notes.map((noteId) =>
          this.notesStore.getNoteList(noteId)
        );
        this.notes.pop();
      }
    },
  },
  setup() {
    const noteListStore = useNotesListStore();
    const notesStore = useNoteStore();
    return { noteListStore, notesStore };
  },
  mounted() {
    this.updateNotes();
  },
  watch: {
    "$route.params.id": {
      handler(newVal, oldVal) {
        if (newVal !== oldVal) {
          this.updateNotes();
        }
      },
      immediate: true,
    },
  },
  computed: {},
};
</script>
<style scoped></style>
