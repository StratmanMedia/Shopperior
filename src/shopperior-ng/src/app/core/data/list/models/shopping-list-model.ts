import { Guid } from "src/app/shared/classes/guid";
import { ListItemModel } from "./list-tem-model";

export class ShoppingListModel {
  guid: string;
  name: string;
  description: string;
  items: ListItemModel[] = [];
}