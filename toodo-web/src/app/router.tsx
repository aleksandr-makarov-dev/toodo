import React from 'react';
import { BrowserRouter, Route, Routes } from 'react-router';
import { paths } from '../config/paths';

const IssuesPage = React.lazy(() => import('../pages/issues-page'));

const IssuePage = React.lazy(() => import('../pages/issue-page'));

export function AppRouter() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path={paths.issues.root.path}>
          <Route index element={<IssuesPage />} />
          <Route path={paths.issues.id.path} element={<IssuePage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
