import { ListItemModel } from "./list-tem-model";

export class ShoppingListModel {
  guid: string;
  name: string;
  items: ListItemModel[] = [];
}