import React from "react";
import { useNavigate } from "react-router-dom";
import type { GridColumnViewModel } from "../viewModels/baseViewModel";

interface FetchData<T> {
  data: T[];
  columns:GridColumnViewModel[];
  pageNumber: number;
  totalPages: number;
  setPageNumber: React.Dispatch<React.SetStateAction<number>>;
  showEdit?: boolean;
  editUrl?: string;
  showDelete?: boolean;
  setDelete?: (id: string) => Promise<void>; 
}

function Grid<T extends Record<string, any>>({
  data,
  columns,
  pageNumber,
  totalPages,
  setPageNumber,
  showEdit,
  editUrl,
  showDelete,
  setDelete,
}: FetchData<T>) {
  const navigate = useNavigate();

  if (!data || data.length === 0) return <div>No data available</div>;

  const navigateToEdit = (id: string) => {
    if (editUrl) {
      navigate(`${editUrl}/${id}`);
    }
  };

  // const columns = Object.keys(data[0]).filter((x) => x !== "id");

  return (
    <div className="space-y-4">
      <table className="min-w-full border border-gray-300">
        <thead>
          <tr>
            {columns.map((col) => (
              <th key={col.name} className="border px-4 py-2 text-left">
                {col.title}
              </th>
            ))}
            {(showEdit || showDelete) && <th className="border px-4 py-2">Actions</th>}
          </tr>
        </thead>
        <tbody>
          {data.map((row, rowIndex) => (
            <tr key={rowIndex}>
              {columns.map((col) => (
                <td key={col.name} className="border px-4 py-2">
                  {row[col.name] !== undefined  && row[col.name] !== null? String(row[col.name]) : ''}
                </td>
              ))}

              {(showEdit || showDelete) && (
                <td className="border px-4 py-2 space-x-2">
                  {showEdit && (
                    <button
                      type="button"
                      onClick={() => navigateToEdit(row.id)}
                      className="bg-blue-500 text-white px-2 py-1 rounded"
                    >
                      Edit
                    </button>
                  )}

                  {showDelete && setDelete && (
                    <button
                      type="button"
                      onClick={() => setDelete(row.id)}
                      className="bg-red-500 text-white px-2 py-1 rounded"
                    >
                      Delete
                    </button>
                  )}
                </td>
              )}
            </tr>
          ))}
        </tbody>
      </table>

      {/* Pagination Controls */}
      <div className="flex items-center justify-center space-x-2">
        <button
          disabled={pageNumber === 1}
          onClick={() => setPageNumber(1)}
          className="px-3 py-1 border rounded disabled:opacity-50"
        >
          First
        </button>
        <button
          disabled={pageNumber === 1}
          onClick={() => setPageNumber((p) => Math.max(1, p - 1))}
          className="px-3 py-1 border rounded disabled:opacity-50"
        >
          Previous
        </button>
        <span className="px-3 py-1">
          Page {pageNumber} of {totalPages}
        </span>
        <button
          disabled={pageNumber === totalPages}
          onClick={() => setPageNumber((p) => Math.min(totalPages, p + 1))}
          className="px-3 py-1 border rounded disabled:opacity-50"
        >
          Next
        </button>
        <button
          disabled={pageNumber === totalPages}
          onClick={() => setPageNumber(totalPages)}
          className="px-3 py-1 border rounded disabled:opacity-50"
        >
          Last
        </button>
      </div>
    </div>
  );
}

export default Grid;
