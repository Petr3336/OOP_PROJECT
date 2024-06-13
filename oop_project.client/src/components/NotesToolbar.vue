<template>
  <v-navigation-drawer
    permanent
    rail
    location="right"
  >
    <v-list density="compact" nav>
      <NewNoteDialog />
      <v-list-item
        prepend-icon="mdi-note-edit"
        @click="$emit('editMode')"
        v-tooltip="'Перейти в режим изменения заметок'"
        :disabled="noNotes"
      ></v-list-item>
      <v-list-item
        prepend-icon="mdi-note-minus"
        @click="$emit('deleteMode')"
        v-tooltip="'Перейти в режим удаления заметок'"
        :disabled="noNotes"
      ></v-list-item>
    </v-list>

    <!-- <div v-else class="d-flex justify-center">
      <v-btn flat icon="mdi-note-plus" />
      <v-btn flat icon="mdi-note-edit" />
      <v-btn flat icon="mdi-note-minus" />
    </div> -->
  </v-navigation-drawer>
</template>
<script>
import NewNoteDialog from "./NewNoteDialog.vue";
export default {
  components: {
    NewNoteDialog,
  },
  name: "NotesToolbar",
  props: {
    noNotes: {
      required: false,
      type: Boolean
    }
  },
  setup() {},
  mounted() {},
  methods: {},
  computed: {},
};
</script>
<style scoped></style>
