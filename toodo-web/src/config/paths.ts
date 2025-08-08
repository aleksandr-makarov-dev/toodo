export const paths = {
  issues: {
    root: {
      path: '/issues',
    },
    index: {
      getHref: () => '/issues',
    },
    id: {
      path: ':id',
      getHref: (id: string) => `/issues/${id}`,
    },
  },
};
