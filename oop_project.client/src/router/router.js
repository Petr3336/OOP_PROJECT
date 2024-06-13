import { createRouter, createWebHistory } from "vue-router";
const IntefaceView = () => import('../views/InterfaceView.vue');
const Login = () => import("../views/LoginView.vue");
const RegisterView = () => import("../views/RegisterView.vue")
const FolderView = () => import("../views/FolderView.vue");
const NoteListView = () => import("../views/NoteListView.vue");

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "home",
      meta: {requiresAuth: true},
      component: IntefaceView,
      children: [
        {
          path: "/folder/:id(\\d+)",
          name: "FolderView",
          component: FolderView,
        },
        {
          path: "/note/:id(\\d+)",
          name: "NoteListView",
          component: NoteListView,
        },
      ],
    },
    { path: "/login/", name: "LoginView", component: Login },
    { path: "/register", name: "RegisterView", component: RegisterView },

    //{ path: '/about', name: 'about', component: AboutView }
    // Добавьте другие маршруты здесь
  ],
});

// router.beforeEach((to, from, next) => {
//   if (false) {
//   } else {
//     next(); // всегда так или иначе нужно вызвать next()!
//   }
// });

router.beforeEach((to, from, next) => {
  if(to.matched.some(record => record.meta.requiresAuth)) {
      if (localStorage.getItem('accessToken') == null) {
          next({
              name: "LoginView",
              params: { nextUrl: to.fullPath }
          })
          return;
      }
      next()
      return;
  }
  next()
})


export default router;
