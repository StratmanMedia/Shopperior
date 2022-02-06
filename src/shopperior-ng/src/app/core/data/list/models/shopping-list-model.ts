import { ListItemModel } from "./list-tem-model";
import { UserListPermissionModel } from "./user-list-permission-model";

export interface ShoppingListModel {
  guid: string;
  name: string;
  permissions: UserListPermissionModel[];
  items: ListItemModel[];
}