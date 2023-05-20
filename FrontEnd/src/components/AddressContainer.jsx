import { HiOutlineTrash, HiPencil, HiPlusCircle } from 'react-icons/hi';
import AddressForm from './AddressForm';
import useAddress from '../utils/hooks/useAddress';
import useClient from '../utils/hooks/useClient';

const AddressContainer = () => {
  const { clientSelectedToWork } = useClient();
  const {
    createAddressRequest,
    addressList,
    addressCreateMode,
    addressEditMode,
    addressSelectedToWork,
    dispatchToogleAddressCreateMode,
    dispatchToogleAddressEditMode,
    dispatchSetAddressSelectedToWork,
    deleteAddressRequest,
    updateAddressRequest,
  } = useAddress();

  return (
    <div className="border m-1 p-1 rounded-md basis-4/12  border-black flex flex-col text-lg">
      <div className="flex items-center justify-between">
        <h4>Addresses</h4>
        {clientSelectedToWork && (
          <HiPlusCircle
            className="text-green-600 cursor-pointer disabled"
            size={30}
            onClick={() => {
              if (!addressCreateMode) {
                dispatchToogleAddressCreateMode();
              }
            }}
          />
        )}
      </div>
      {addressCreateMode || addressEditMode ? (
        <AddressForm
          editMode={addressEditMode}
          addressSelectedToWork={addressSelectedToWork}
          onCancelSumbit={() => {
            if (addressEditMode) {
              dispatchToogleAddressEditMode();
            }

            if (addressCreateMode) {
              dispatchToogleAddressCreateMode();
            }
          }}
          onSubmit={(data) => {
            if (addressCreateMode) {
              createAddressRequest({
                clientId: clientSelectedToWork.id,
                ...data,
              });
            }
            if (addressEditMode) {
              updateAddressRequest({
                id: addressSelectedToWork.id,
                clientId: clientSelectedToWork.id,
                ...data,
              });
            }
          }}
        />
      ) : (
        <>
          {clientSelectedToWork && addressList.length > 0 && (
            <ul>
              {addressList?.map((address) => (
                <li
                  key={address?.id}
                  className={`cursor-pointer outline  p-1 m-0.5 border-1 rounded-md my-3 ${
                    address?.id === addressSelectedToWork?.id
                      ? 'outline-green-500'
                      : 'outline-slate-200'
                  }`}
                  onClick={(e) => {
                    e.preventDefault();
                    dispatchSetAddressSelectedToWork(address);
                  }}
                >
                  <div className="flex items-center mx-1">
                    <div className="flex-1 min-w-0">
                      <p className="text-sm font-medium text-gray-900 truncate dark:text-white">
                        {address.city}
                      </p>
                      <p className="text-sm text-gray-500 truncate dark:text-gray-400">
                        {address.street}
                      </p>
                      <p className="text-sm text-gray-500 truncate dark:text-gray-400">
                        {address.state}- {address.zip}
                      </p>
                    </div>
                    <div className="flex-col  self-center">
                      {address.id === addressSelectedToWork?.id && (
                        <HiPencil
                          className="text-orange-400 cursor-pointer disabled"
                          size={25}
                          onClick={(e) => {
                            e.stopPropagation();
                            dispatchToogleAddressEditMode();
                          }}
                        />
                      )}

                      <HiOutlineTrash
                        className="text-red-600 cursor-pointer disabled"
                        size={25}
                        onClick={(e) => {
                          e.stopPropagation();
                          deleteAddressRequest(address);
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
export default AddressContainer;
