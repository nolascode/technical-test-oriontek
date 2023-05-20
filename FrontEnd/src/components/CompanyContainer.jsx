import useCompany from '../utils/hooks/useCompany';
import { HiOutlineTrash, HiPencil, HiPlusCircle } from 'react-icons/hi';
import CompanyForm from './CompanyForm';
import useClient from '../utils/hooks/useClient';

const CompanyContainer = () => {
  const {
    companies,
    companySelectedToWork,
    creatingMode,
    editMode,
    dispatchSetCompanySelectedToWork,
    dispatchToogleEditingMode,
    dispatchToogleCreatingMode,
    createCompanyRequest,
    deleteCompanyRequest,
    updateCompanyApiRequest,
  } = useCompany();

  const { dispatchSetClientSelectedToWork } = useClient();

  return (
    <div className="border m-1 p-1 rounded-md basis-4/12 border-black flex flex-col text-lg overflow-auto">
      <div className="flex items-center justify-between">
        <h4>Companies</h4>
        {!creatingMode && (
          <HiPlusCircle
            className="text-green-600 cursor-pointer disabled"
            size={30}
            onClick={() => {
              dispatchToogleCreatingMode();
              dispatchSetCompanySelectedToWork(null);
            }}
          />
        )}
      </div>
      {creatingMode || editMode ? (
        <CompanyForm
          editMode={editMode}
          companySelectedToWork={companySelectedToWork}
          onSubmit={async (values) => {
            if (editMode) {
              dispatchToogleEditingMode();
              updateCompanyApiRequest({
                companyId: companySelectedToWork?.id,
                ...values,
              });
            }
            if (creatingMode) {
              await createCompanyRequest(values);
              dispatchToogleCreatingMode();
            }
          }}
          cancelSumbit={() => {
            if (creatingMode) {
              dispatchToogleCreatingMode();
            }
            if (editMode) {
              dispatchToogleEditingMode();
            }
          }}
        />
      ) : (
        <ul>
          {companies.map((company) => (
            <li
              key={company.id}
              className={`cursor-pointer outline p-1 m-0.5 flex justify-between border-1 rounded-md my-3 ${
                company.id === companySelectedToWork?.id
                  ? 'outline-green-600'
                  : 'outline-slate-200'
              }`}
              onClick={() => {
                dispatchSetCompanySelectedToWork(company);
                dispatchSetClientSelectedToWork(null);

                if (editMode) {
                  dispatchToogleEditingMode();
                }
              }}
            >
              <div className="flex items-center mx-1">
                <div className="flex-1 min-w-0">
                  <p className="text-sm font-medium text-gray-900 truncate dark:text-white">
                    {company.name}
                  </p>
                  <p className="text-sm text-gray-500 truncate dark:text-gray-400">
                    {company.address} {JSON.stringify(editMode)}
                  </p>
                  <p className="text-sm text-gray-500 truncate dark:text-gray-400">
                    {company.email}
                  </p>
                  <p className="text-sm text-gray-500 truncate dark:text-gray-400">
                    {company.phone}
                  </p>
                  <p className="text-sm text-gray-500 truncate dark:text-gray-400">
                    {company.city}
                  </p>
                </div>
              </div>
              <div className="flex-col  self-center">
                {company.id === companySelectedToWork?.id &&
                  editMode === false && (
                    <HiPencil
                      className="text-orange-400 cursor-pointer disabled"
                      size={25}
                      onClick={(e) => {
                        e.stopPropagation();
                        dispatchSetCompanySelectedToWork(company);
                        dispatchToogleEditingMode();
                      }}
                    />
                  )}
                <HiOutlineTrash
                  className="text-red-600 cursor-pointer disabled"
                  size={25}
                  onClick={(e) => {
                    e.stopPropagation();
                    deleteCompanyRequest({
                      companyId: company.id,
                    });
                  }}
                />
              </div>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};

export default CompanyContainer;
