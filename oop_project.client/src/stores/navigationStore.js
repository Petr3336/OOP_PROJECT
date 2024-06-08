import { defineStore } from "pinia";

class Navigation {
  constructor(
    id,
    text,
    position,
    parent,
    childrenNavigation = []
  ) {
    this.id = id;
    this.type = 0;
    this.name = text;
    this.position = position;
    this.parent = parent;
    this.childrenNavigation = childrenNavigation;
  }
}

export const useNavigationStore = defineStore("navigationStore", {
  state: () => ({
    navigation: [
      {
        id: 0,
        type: 0,
        name: "f1",
        position: "0",
        parent: 0,
        childrenNavigation: [
          {
            id: 1,
            type: 0,
            name: "f2",
            position: "0",
            parent: 0,
            childrenNavigation: [],
          },
          {
            id: 2,
            type: 0,
            name: "f3",
            position: "0",
            parent: 0,
            childrenNavigation: [],
          },
          {
            id: 0,
            type: 1,
            name: "n1",
            position: "0",
            parent: 0,
            childrenNavigation: [],
          },
          {
            id: 1,
            type: 1,
            name: "n2",
            position: "0",
            parent: 0,
            childrenNavigation: [],
          },
          {
            id: 1,
            type: 1,
            name: "n2",
            position: "0",
            parent: 0,
            childrenNavigation: [],
          },
          {
            id: 1,
            type: 1,
            name: "n2",
            position: "0",
            parent: 0,
            childrenNavigation: [],
          },
          {
            id: 1,
            type: 1,
            name: "n2",
            position: "0",
            parent: 0,
            childrenNavigation: [],
          },
          {
            id: 1,
            type: 1,
            name: "n2",
            position: "0",
            parent: 0,
            childrenNavigation: [],
          },
          {
            id: 1,
            type: 1,
            name: "n2",
            position: "0",
            parent: 0,
            childrenNavigation: [],
          },
          {
            id: 1,
            type: 1,
            name: "n2",
            position: "0",
            parent: 0,
            childrenNavigation: [],
          },
          {
            id: 1,
            type: 1,
            name: "n2",
            position: "0",
            parent: 0,
            childrenNavigation: [],
          },
          {
            id: 1,
            type: 1,
            name: "n2",
            position: "0",
            parent: 0,
            childrenNavigation: [],
          },
          {
            id: 1,
            type: 1,
            name: "n2",
            position: "0",
            parent: 0,
            childrenNavigation: [],
          },
          {
            id: 1,
            type: 1,
            name: "n2",
            position: "0",
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
    createNavigation(id, text, position, parent) {
      const newNavigation = new Navigation(id, text, position, parent);
      this.navigation.push(newNavigation);
      return newNavigation;
    },
  },
});
