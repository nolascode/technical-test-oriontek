import useCompany from '../utils/hooks/useCompany';
import { HiOutlineTrash, HiPencil, HiPlusCircle } from 'react-icons/hi';
import ClientForm from './ClientForm';
import useClient from '../utils/hooks/useClient';
const ClientContainer = () => {
  const { companySelectedToWork } = useCompany();

  const {
    clients,
    clientEditMode,
    clientCreateMode,
    clientSelectedToWork,
    createClientRequest,
    deleteClientRequest,
    updateClientRequest,
    dispatchSetClientSelectedToWork,
    dispatchToogleClientEditMode,
    dispatchToogleClientCreateMode,
  } = useClient();

  return (
    <div className="border m-1 p-1 rounded-md basis-4/12  border-black flex flex-col text-lg">
      <div className="flex items-center justify-between">
        <h4>Clients</h4>
        {companySelectedToWork && !clientEditMode && (
          <HiPlusCircle
            className="text-green-600 cursor-pointer disabled"
            size={30}
            onClick={() => {
              dispatchToogleClientCreateMode();
            }}
          />
        )}
      </div>
      {clientCreateMode || clientEditMode ? (
        <ClientForm
          editMode={clientEditMode}
          clientSelectedToWork={clientSelectedToWork}
          cancelSumbit={() => {
            if (clientEditMode) {
              dispatchToogleClientEditMode();
            }
            if (clientCreateMode) {
              dispatchToogleClientCreateMode();
            }
          }}
          onSubmit={(formData) => {
            if (clientCreateMode) {
              createClientRequest({
                companyId: companySelectedToWork.id,
                ...formData,
              });
            }

            if (clientEditMode) {
              updateClientRequest({
                clientId: clientSelectedToWork?.id,
                companyId: companySelectedToWork.id,
                ...formData,
              });
            }
          }}
        />
      ) : (
        <>
          {companySelectedToWork && clients.length > 0 && (
            <ul>
              {clients?.map((client) => (
                <li
                  key={client?.id}
                  className={`cursor-pointer outline  p-1 m-0.5 border-1 rounded-md my-3 ${
                    client?.id === clientSelectedToWork?.id
                      ? 'outline-green-500'
                      : 'outline-slate-200'
                  }`}
                  onClick={(e) => {
                    e.preventDefault();
                    e.stopPropagation();
                    dispatchSetClientSelectedToWork(client);
                  }}
                >
                  <div className="flex items-center mx-1">
                    <div className="flex-1 min-w-0">
                      <p className="text-sm font-medium text-gray-900 truncate dark:text-white">
                        {client.name}
                      </p>
                      <p className="text-sm text-gray-500 truncate dark:text-gray-400">
                        {client.email}
                      </p>
                      <p className="text-sm text-gray-500 truncate dark:text-gray-400">
                        {client.phone}
                      </p>
                    </div>
                    <div className="flex-col  self-center">
                      {client.id === clientSelectedToWork?.id && (
                        <HiPencil
                          className="text-orange-400 cursor-pointer disabled"
                          size={25}
                          onClick={(e) => {
                            e.stopPropagation();
                            dispatchToogleClientEditMode();
                          }}
                        />
                      )}

                      <HiOutlineTrash
                        className="text-red-600 cursor-pointer disabled"
                        size={25}
                        onClick={(e) => {
                          e.stopPropagation();
                          deleteClientRequest({
                            clientId: client?.id,
                          });
                        }}
                      />
                    </div>
                  </div>
                </li>
              ))}
            </ul>
          )}
        </>
      )}
    </div>
  );
};

export default ClientContainer;
