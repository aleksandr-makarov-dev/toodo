import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import type { CreateIssue, Issue } from './types';

export const issuesApi = createApi({
  reducerPath: 'issues-api',
  baseQuery: fetchBaseQuery({
    baseUrl: 'https://jsonplaceholder.typicode.com',
  }),
  tagTypes: ['posts'],
  endpoints: (builder) => ({
    getIssues: builder.query<Issue[], void>({
      query: () => '/posts',
      providesTags: ['posts'],
    }),
    getIssue: builder.query<Issue, { id: string }>({
      query: ({ id }) => `/posts/${id}`,
    }),
    createIssue: builder.mutation<Issue, CreateIssue>({
      query: (data) => ({ url: '/posts', method: 'POST', body: data }),
      invalidatesTags: ['posts'],
    }),
  }),
});

export const { useGetIssuesQuery, useGetIssueQuery, useCreateIssueMutation } =
  issuesApi;
