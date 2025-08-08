export type Issue = {
  id: number;
  userId: number;
  title: string;
  body: string;
};

export type CreateIssue = {
  title: string;
  body: string;
};
