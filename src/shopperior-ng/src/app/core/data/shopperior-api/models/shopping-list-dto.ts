import { CategoryDto } from "./category-dto";
import { ListItemDto } from "./list-item-dto";
import { UserListPermissionDto } from "./user-list-permission-dto";

export interface ShoppingListDto {
  guid: string;
  name: string;
  permissions: UserListPermissionDto[];
  categories: CategoryDto[];
  items: ListItemDto[];
}