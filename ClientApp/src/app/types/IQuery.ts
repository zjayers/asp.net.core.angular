export interface IQuery {
  page: number;
  pageSize: number;
  makeId: string;
  modelId: string;
  sortBy: string;
  isSortAscending: boolean;
}
