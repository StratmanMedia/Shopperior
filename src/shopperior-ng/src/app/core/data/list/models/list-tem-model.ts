import { ItemModel } from "../../item/models/item-model";

export class ListItemModel {
  item: ItemModel;
  location: string;
  quantity: number;
  units: string;
  unitPrice: number;
  subtotal: number;
}