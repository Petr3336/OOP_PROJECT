<template>
  <v-card
        variant="elevated"
        class="mx-auto"
        width="90%"
      >
        <v-card-item >
          <div>
            <div class="text-overline mb-1">
              
            </div>
            <div class="text-h6 mb-1">
              {{ note ? note.name : "Здесь пока пусто" }}
            </div>
            <div class="text-caption">{{ note ? note.text : "Создайте свою первую заметку" }}</div>
          </div>
        </v-card-item>

        <v-card-actions>
            <v-checkbox label="Checkbox" v-if="note" v-model="note.completed"></v-checkbox>
        </v-card-actions>
      </v-card>
</template>
<script>
export default {
  name: "NoteComponent",
  props: {
    note: {
      required: true,
      type: Array,
    },
  },
  setup() {
  },
  mounted() {
  },
  components: {},
  methods: {},
  computed: {
  },
};
</script>
<style scoped></style>
