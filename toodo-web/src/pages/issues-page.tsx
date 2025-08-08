import { NavLink } from 'react-router';
import {
  useCreateIssueMutation,
  useGetIssuesQuery,
} from '../features/issues/api';
import { paths } from '../config/paths';

function IssuesPage() {
  const issuesQuery = useGetIssuesQuery();
  const [createIssue, issueMutation] = useCreateIssueMutation();

  const handleCreateIssue = () => {
    createIssue({
      title: 'New test isssue',
      body: 'New test issue body description',
    });
  };

  if (issuesQuery.isLoading) return <p>Loading...</p>;

  if (issuesQuery.isError)
    return (
      <div>
        <p>Could not fetch issues</p>
        <button onClick={() => issuesQuery.refetch()}>refetch</button>
      </div>
    );

  return (
    <div>
      <button onClick={handleCreateIssue}>
        {issueMutation.isLoading ? 'Creating...' : 'Create issue'}
      </button>
      <ul>
        {issuesQuery.data?.map((item) => (
          <li key={item.id}>
            <p>
              <span>{item.id}</span>{' '}
              <NavLink to={paths.issues.id.getHref(item.id.toString())}>
                {item.title}
              </NavLink>{' '}
              <span>[{item.userId}]</span>
            </p>
            <p>{item.body}</p>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default IssuesPage;
