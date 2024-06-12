<template>
  <v-main class="d-flex justify-center align-center">
    <v-card
      class="mx-auto pa-12 pb-2 flex-grow-1"
      elevation="8"
      max-width="448"
      rounded="lg"
    >
      <v-form @submit.prevent>
        <template v-slot:default="form">
          <div class="text-subtitle-1 text-medium-emphasis">Аккаунт</div>

          <v-text-field
            v-model="email"
            density="compact"
            placeholder="Почтовый адрес"
            prepend-inner-icon="mdi-email-outline"
            variant="outlined"
            :rules="emailRules"
          ></v-text-field>

          <div
            class="text-subtitle-1 text-medium-emphasis d-flex align-center justify-space-between"
          >
            Пароль

            <!-- <a
              class="text-caption text-decoration-none text-blue"
              href="#"
              rel="noopener noreferrer"
              target="_blank"
            >
              Забыли пароль?</a
            > -->
          </div>

          <v-text-field
            v-model="password"
            :append-inner-icon="visible ? 'mdi-eye-off' : 'mdi-eye'"
            :type="visible ? 'text' : 'password'"
            density="compact"
            placeholder="Введите ваш пароль"
            prepend-inner-icon="mdi-lock-outline"
            variant="outlined"
            @click:append-inner="visible = !visible"
            :rules="passwordRules"
          ></v-text-field>

          <!-- <v-card class="mb-12" color="surface-variant" variant="tonal">
            <v-card-text class="text-medium-emphasis text-caption">
              Warning: After 3 consecutive failed login attempts, you account
              will be temporarily locked for three hours. If you must login now,
              you can also click "Forgot login password?" below to reset the
              login password.
            </v-card-text>
          </v-card> -->

          <v-card class="mb-4" color="surface-variant" variant="tonal">
          </v-card>

          <v-btn
            class="mb-2"
            color="blue"
            size="large"
            variant="tonal"
            block
            :disabled="!form.isValid.value"
            @click="register()"
          >
            Зарегистрироваться
          </v-btn>

          <v-card-text class="text-center">
            <RouterLink class="text-blue text-decoration-none mr-3" to="/login">
              <v-icon icon="mdi-chevron-left"></v-icon> Войти
            </RouterLink>
          </v-card-text>
        </template>
      </v-form>
    </v-card>
  </v-main>
</template>

<script>
import axios from "axios";

export default {
  name: "RegisterView",
  data: () => ({
    visible: false,
    email: "",
    password: "",
    emailRules: [
      (v) => !!v || "Почта необходима для входа",
      (v) =>
        /^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$/.test(v) ||
        "Почтовый адресс не корректный",
    ],
    passwordRules: [
      (v) => !!v || "Пароль не может отсутствовать",
      (v) =>
        !/[^ -~]/.test(v) ||
        "В пароле должны присутствовать только символы латинского алфавита, цифры, и спецсимволы",
      (v) =>
        /[A-Z]/.test(v) ||
        "В пароле должен быть как минимум один заглавный латинский символ",
      (v) => /\d/.test(v) || "В пароле должен быть хотя-бы один символ цифры",
      (v) =>
        /[!@#$%^&*()_+\-=[\]{};':"\\|,.<>/?]/.test(v) ||
        "В пароле должен присутствовать хотя-бы один спец символ",
      (v) => v?.length >= 6 || "Пароль слишком короткий",
    ],
  }),
  methods: {
    register() {
      axios
        .post("/api/register", {
          email: this.email,
          password: this.password,
        })
        .then((response) => {
          if (response.status != 200) return;
          this.$router.push({ name: "LoginView" });
        })
        .catch((error) => {
          alert(
            error.response.data.errors[
              Object.keys(error.response.data.errors)[0]
            ]
          );
          throw "registerError";
        });
    },
  },
};
</script>
