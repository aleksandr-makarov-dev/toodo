import { NavLink, useParams } from 'react-router';
import { useGetIssueQuery } from '../features/issues/api';
import { paths } from '../config/paths';

function IssuePage() {
  const { id = '' } = useParams<{ id: string }>();

  const issueQuery = useGetIssueQuery({ id });

  if (issueQuery.isLoading) return <p>Loading...</p>;

  if (issueQuery.isError)
    return (
      <div>
        <p>Failed to fetch issue with id = {id}</p>
        <button onClick={() => issueQuery.refetch()}>refetch</button>
      </div>
    );

  if (!issueQuery.data) return <p>Issue not found</p>;

  return (
    <div>
      <NavLink to={paths.issues.index.getHref()}>return</NavLink>
      <p>{issueQuery.data.title}</p>
      <p>{issueQuery.data.description}</p>
    </div>
  );
}

export default IssuePage;
