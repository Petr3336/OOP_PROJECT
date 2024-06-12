import { defineStore } from "pinia";

class Navigation {
  constructor(id, text, type, position) {
    this.id = id;
    this.name = text;
    this.type = type;
    this.position = position;
    this.childrenNavigation = [];
  }
}

export const useNavigationStore = defineStore("NavigationStore", {
  state: () => ({
    navigation: [
      {
        id: 0,
        type: 0,
        name: "f1",
        collapsed: false,
        position: 0,
        parent: 0,
        childrenNavigation: [
          {
            id: 1,
            type: 0,
            name: "f2",
            collapsed: false,
            position: 0,
            parent: 0,
            childrenNavigation: [],
          },
          {
            id: 2,
            type: 0,
            name: "f3",
            collapsed: false,
            position: 0,
            parent: 0,
            childrenNavigation: [],
          },
          {
            id: 0,
            type: 1,
            name: "n1",
            position: 0,
            parent: 0,
            childrenNavigation: [],
          },
          {
            id: 1,
            type: 1,
            name: "n2",
            position: 0,
            parent: 0,
            childrenNavigation: [],
          },
        ],
      },
    ],
  }),
  actions: {
    getNavigationById(id) {
      // Вспомогательная функция для рекурсивного поиска
      function searchFolder(navigationArray, id) {
        for (const folder of navigationArray) {
          if (folder.id == id) {
            return folder;
          }
          if (folder.childrenNavigation.length) {
            const found = searchFolder(folder.childrenNavigation, id);
            if (found) {
              return found;
            }
          }
        }
        return null;
      }
      // Начинаем поиск с корневого массива navigation
      return searchFolder(this.navigation, id);
    },
    updateNavigationPosition(id, newPosition) {
      const navigation = this.getNavigationById(id);
      if (navigation) {
        navigation.position = newPosition;
      }
    },
    createNoteNavigation(id, text) {
      let positon = this.navigation.length;
      const newNavigation = new Navigation(id, text, 1, positon);
      this.navigation.push(newNavigation);
      return newNavigation;
    },
  },
});
