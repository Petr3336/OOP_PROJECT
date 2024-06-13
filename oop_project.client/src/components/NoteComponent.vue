<template>
  <v-card variant="elevated" class="" width="90%">

    <!-- В режиме просмотра -->
    <v-card-item v-if="!editing">
      <div :class="note ? '' : 'pb-6 pt-3'">
        <div class="text-overline mb-1"></div>
        <div class="text-h6 mb-1">
          {{ note ? note.name : "Здесь пока пусто" }}
        </div>
        <div class="">
          {{ note ? note.description : "Создайте свою первую заметку" }}
        </div>
      </div>
    </v-card-item>

    <v-card-actions v-if="note && !editing" class="mt-n2 mb-2">
      <v-checkbox-btn
        label="Checkbox"
        v-bind:model-value="note.completed"
        readonly
        @click="notesStore.changeNoteStatus(note.id, note.completed)"
        :disabled="deleting"
      ></v-checkbox-btn>
      <v-btn
        v-if="deleting"
        flat
        size="38px"
        color="error"
        icon="mdi-delete"
        v-tooltip="'Удалить заметку'"
        @click="notesStore.removeNote(note.id)"
      >
      </v-btn>
    </v-card-actions>

    <!-- В режиме редактирования -->
    <v-confirm-edit
      v-if="note && editing"
      :model-value="note"
      @update:model-value="(val) => notesStore.updateNote(val)"
    >
      <template
        v-slot:default="{
          model: proxyModel,
          save,
          cancel,
          isPristine,
          actions
        }"
      >
        <v-card-item>
          <div>
            <div class="text-overline mb-1"></div>
            <div class="text-h6">
              <v-text-field
                label="Название вашей заметки"
                v-model="proxyModel.value.name"
                :rules="noteNameRules"
                variant="underlined"
                required
                color="primary"
              ></v-text-field>
            </div>
            <div class="text-caption">
              <v-textarea
                label="Описание вашей заметки"
                rows="1"
                class=""
                v-model="proxyModel.value.description"
                variant="underlined"
                auto-grow
                counter
                color="primary"
              ></v-textarea>
            </div>
          </div>
        </v-card-item>
        <v-card-actions class="mt-n6 mb-2">
          <v-checkbox-btn
            label="Checkbox"
            v-model="proxyModel.value.completed"
          ></v-checkbox-btn>
          <v-btn
            flat
            size="38px"
            color="error"
            icon="mdi-close"
            v-tooltip="'Сохранить изменения'"
            @click="cancel()"
            :disabled="isPristine"
          >
          </v-btn>
          <v-btn
            flat
            size="38px"
            color="success"
            icon="mdi-check"
            v-tooltip="'Сохранить изменения'"
            @click="save()"
            :disabled="isPristine"
          >
          </v-btn>
        </v-card-actions>
      </template>
    </v-confirm-edit>
  </v-card>
</template>
<script>
import { useNoteStore } from "../stores/NoteStore";
export default {
  name: "NoteComponent",
  props: {
    note: {
    },
    editing: {
      type: Boolean,
    },
    deleting: {
      type: Boolean,
    },
  },
  data: () => ({
    noteNameRules: [
      (value) => !!value || "У заметки должен быть заголовок",
      (value) =>
        (value && value.length >= 3) ||
        "Минимальная длинна названия - 3 символа",
    ],
  }),
  setup() {
    const notesStore = useNoteStore();
    return { notesStore };
  },
  mounted() {},
  components: {},
  methods: {},
  computed: {},
};
</script>
<style scoped></style>
