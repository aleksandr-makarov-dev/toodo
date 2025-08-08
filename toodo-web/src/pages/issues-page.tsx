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
      description: 'New test issue body description',
    });
  };

  if (issuesQuery.isLoading) return <p>Loading...</p>;

  if (issuesQuery.isError)
    return (
      <div>
        <p>{issuesQuery.error.toString()}</p>
        <button onClick={() => issuesQuery.refetch()}>
          {issuesQuery.isFetching ? 'refetching...' : 'refetch'}
        </button>
      </div>
    );

  return (
    <div>
      <div>
        <button onClick={handleCreateIssue}>
          {issueMutation.isLoading ? 'Creating...' : 'Create issue'}
        </button>
        <button onClick={() => issuesQuery.refetch()}>
          {issuesQuery.isFetching ? 'refetching...' : 'refetch'}
        </button>
      </div>
      <ul>
        {issuesQuery.data?.map((item) => (
          <li key={item.id}>
            <p>
              <span>{item.id}</span>{' '}
              <NavLink to={paths.issues.id.getHref(item.id.toString())}>
                {item.title}
              </NavLink>{' '}
            </p>
            <p>{item.description}</p>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default IssuesPage;
