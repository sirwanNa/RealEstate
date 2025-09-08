import type { ChangeEvent } from "react";
import type { SelectListItem } from "../viewModels/realEstate/propertyInventory/propertyInventoryModel";

const Dropdown: React.FC<{
  label: string;
  name: string;
  value?: string | number | null | undefined;
  options?: SelectListItem[];
  onChange: ( e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => void;
}> = ({ label, name, value, options, onChange }) => {
  if (!options || options.length === 0) return null;

  return (
    <div className="mb-4">
      <label className="block mb-1 font-medium">{label}</label>
      <select
        name={name}
        className="border rounded px-2 py-1 w-full"
        value={value ?? ""}
        onChange={onChange}
      >
        <option value="">-- Select {label} --</option>
        {options.map((opt) => (
          <option key={opt.value} value={opt.value}>
            {opt.text}
          </option>
        ))}
      </select>
    </div>
  );
};

export default Dropdown;